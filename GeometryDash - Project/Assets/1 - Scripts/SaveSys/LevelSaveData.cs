using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class LevelSaveData : MonoBehaviour
{
    public GameRules gameRules;

    string saveSeparator = "%LEVELDATA%";
    string encryptionKey = "^D_N=G^$SHK6k_1PP#4ocH@=o)2cDaNQ";

    [HideInInspector] public int gloablScore;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        LoadGlobalScore();
    }

    public void ApplyAndSaveBestScore()
    {
        string filePath = Application.dataPath + "/levelData.txt";
        int bestScore = 0;

        if (File.Exists(filePath))
        {
            string encryptedData = File.ReadAllText(filePath);
            string saveString = Decrypt(encryptedData, encryptionKey);
            string[] content = saveString.Split(new[] { saveSeparator }, StringSplitOptions.None);

            if (content.Length > 1)
            {
                int.TryParse(content[0], out gloablScore);
                int.TryParse(content[1], out bestScore);
            }
        }

        gloablScore += gameRules.playerLevelScore;

        if (gameRules.playerLevelScore > bestScore)
        {
            bestScore = gameRules.playerLevelScore;
        }

        string[] contentToSave = new string[]
        {
            gloablScore.ToString(),
            bestScore.ToString()
        };

        string saveStringUpdated = string.Join(saveSeparator, contentToSave);
        string encryptedDataUpdated = Encrypt(saveStringUpdated, encryptionKey);
        File.WriteAllText(filePath, encryptedDataUpdated);

        gameRules.bestScoreDisplay.text = "Meilleur score : " + bestScore;
    }

    public int LoadBestScore()
    {
        string filePath = Application.dataPath + "/levelData.txt";
        int bestScore = 0;

        if (File.Exists(filePath))
        {
            string encryptedData = File.ReadAllText(filePath);
            string saveString = Decrypt(encryptedData, encryptionKey);
            string[] content = saveString.Split(new[] { saveSeparator }, StringSplitOptions.None);

            if (content.Length > 1 && int.TryParse(content[1], out bestScore))
            {
                return bestScore;
            }
        }
        return bestScore;
    }

    public int LoadGlobalScore()
    {
        string filePath = Application.dataPath + "/levelData.txt";
        int loadedGlobalScore = 0;

        if (File.Exists(filePath))
        {
            string encryptedData = File.ReadAllText(filePath);
            string saveString = Decrypt(encryptedData, encryptionKey);
            string[] content = saveString.Split(new[] { saveSeparator }, StringSplitOptions.None);

            if (content.Length > 0 && int.TryParse(content[0], out loadedGlobalScore))
            {
                gloablScore = loadedGlobalScore;
                return loadedGlobalScore;
            }
        }
        return loadedGlobalScore;
    }

    private string Encrypt(string plainText, string key)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = new byte[16];

            using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                return System.Convert.ToBase64String(encryptedBytes);
            }
        }
    }

    public string Decrypt(string cipherText, string key)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = new byte[16];

            using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
            {
                byte[] cipherBytes = System.Convert.FromBase64String(cipherText);
                byte[] plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

                return Encoding.UTF8.GetString(plainBytes);
            }
        }
    }
}
