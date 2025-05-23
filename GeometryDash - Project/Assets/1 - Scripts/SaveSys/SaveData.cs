using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;

public class SaveData : MonoBehaviour
{
    public SO_PlayerStat playerStat;
    [SerializeField] PlayerInventory _inventory;
    [SerializeField] TMP_InputField pseudoField;

    static string saveSeparator = "%DATA%";
    static string encryptionKey = "^D_N=G^$SHK6k_1PP#4ocH@=o)2cDaNQ"; // Clé de 32 caractères

    static readonly string[] prefixes = { "Shadow", "Cyber", "Neo", "Dark", "Sky", "Ghost", "Lunar", "Storm", "Pixel", "Phantom" };
    static readonly string[] suffixes = { "Wolf", "Ninja", "X", "Eclipse", "Fury", "Hunter", "Knight", "Storm", "Rider", "Fox" };


    //-------------------
    //  METHODES DEFAULT
    //-------------------

    void Awake()
    {
        if (playerStat.openSave == true)
        {
            load();
        }

        pseudoField.text = playerStat.pseudo;
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    save();
        //}

        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    load();
        //}
    }


    //-------------------
    //  METHODES PUBLIC
    //-------------------

    public void save()
    {
        bool[] skins = playerStat.possesionId;
        string skinsString = string.Join(",", skins);

        string[] content = new string[]
        {
            playerStat.gold.ToString(),
            playerStat.cash.ToString(),
            playerStat.stars.ToString(),
            playerStat.starsCoins.ToString(),

            playerStat.XP.ToString(),
            playerStat.playerLevel.ToString(),
            playerStat.storyProgression.ToString(),
            playerStat.openSave.ToString(),

            skinsString,
            playerStat.actualSkinId.ToString(),
            playerStat.pseudo.ToString(),

            // LevelData ToDO
            playerStat.currentScore.ToString(),
            playerStat.globalScore.ToString(),
            playerStat.levelScore[0].ToString(),
            playerStat.levelScore[1].ToString(),
            playerStat.levelScore[2].ToString(),
            playerStat.levelScore[3].ToString(),
            playerStat.levelScore[4].ToString()
    };

        string saveString = string.Join(saveSeparator, content);

        // Chiffrement des données
        string encryptedData = Encrypt(saveString, encryptionKey);

        File.WriteAllText(Application.dataPath + "/data.txt", encryptedData);
        Debug.Log("Sauvegarde OK");
    }

    public void load()
    {
        if (File.Exists(Application.dataPath + "/data.txt"))
        {
            string encryptedData = File.ReadAllText(Application.dataPath + "/data.txt");

            // Déchiffrement des données
            string saveString = Decrypt(encryptedData, encryptionKey);

            string[] content = saveString.Split(new[] { saveSeparator }, System.StringSplitOptions.None);
            playerStat.gold = float.Parse(content[0]);
            playerStat.cash = float.Parse(content[1]);
            playerStat.stars = float.Parse(content[2]);
            playerStat.starsCoins = float.Parse(content[3]);

            playerStat.XP = int.Parse(content[4]);
            playerStat.playerLevel = int.Parse(content[5]);
            playerStat.storyProgression = int.Parse(content[6]);
            playerStat.openSave = bool.Parse(content[7]);

            string skinsString = content[8];
            string[] skins = skinsString.Split(',');

            playerStat.possesionId = Array.ConvertAll(skins, bool.Parse);

            playerStat.actualSkinId = int.Parse(content[9]);
            playerStat.pseudo = content[10];

            // LevelData ToDo
            playerStat.currentScore = int.Parse(content[11]);
            playerStat.globalScore = int.Parse(content[12]);
            playerStat.levelScore[0] = int.Parse(content[13]);
            playerStat.levelScore[1] = int.Parse(content[14]);
            playerStat.levelScore[2] = int.Parse(content[15]);
            playerStat.levelScore[3] = int.Parse(content[16]);
            playerStat.levelScore[4] = int.Parse(content[17]);


            _inventory.saveCall();

            Debug.Log("Chargement OK");
        }
        else
        {
            Debug.Log("Aucune sauvegarde");
        }
    }

    public (string pseudo, int skinId) GetPlayerData()
    {
        return (playerStat.pseudo, playerStat.actualSkinId);
    }

    public void OnPseudoChange()
    {
        playerStat.pseudo = pseudoField.text;

        if (string.IsNullOrEmpty(playerStat.pseudo))
        {
            string randomPseudo = prefixes[UnityEngine.Random.Range(0, prefixes.Length)] + " " + suffixes[UnityEngine.Random.Range(0, suffixes.Length)];
            playerStat.pseudo = randomPseudo;
        }
    }



    //-------------------
    //  METHODES PRIVEE
    //-------------------

    // Méthode de chiffrement AES
    private string Encrypt(string plainText, string key)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = new byte[16]; // Vecteur d'initialisation à 0 (simple mais pas recommandé pour production)

            using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                return System.Convert.ToBase64String(encryptedBytes);
            }
        }
    }

    // Méthode de déchiffrement AES
    private string Decrypt(string cipherText, string key)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = new byte[16]; // Même vecteur d'initialisation que pour le chiffrement

            using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
            {
                byte[] cipherBytes = System.Convert.FromBase64String(cipherText);
                byte[] plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

                return Encoding.UTF8.GetString(plainBytes);
            }
        }
    }
}
