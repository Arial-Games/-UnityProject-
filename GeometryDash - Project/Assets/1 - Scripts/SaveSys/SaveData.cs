using System.IO;
using UnityEngine;
using TMPro;
using System;

public class SaveData : MonoBehaviour
{
    public SO_PlayerStat playerStat;
    [SerializeField] PlayerInventory _inventory;

    string saveSeparator = "%DATA%";

    void Awake()
    {
        if (playerStat.openSave == true)
        {
            load();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            save();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            load();
        }

    }

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

            skinsString

            // Rajoute la save des skins
    };

        string saveString = string.Join(saveSeparator, content);

        File.WriteAllText(Application.dataPath + "/data.txt", saveString);
        Debug.Log("Sauvegarde OK");
    }

    public void load()
    {
        if (File.Exists("/data.txt"))
        {
            string saveString = File.ReadAllText(Application.dataPath + "/data.txt");

            string[] content = saveString.Split(new[] { saveSeparator }, System.StringSplitOptions.None);
            playerStat.gold = float.Parse(content[0]);
            playerStat.cash = float.Parse(content[1]);
            playerStat.stars = float.Parse(content[2]);
            playerStat.starsCoins = float.Parse(content[3]);

            playerStat.XP = int.Parse(content[4]);
            playerStat.playerLevel = int.Parse(content[5]);
            playerStat.storyProgression = int.Parse(content[6]);
            playerStat.openSave = bool.Parse(content[7]);

            // Rajoute le load des skins
            string skinsString = content[8];
            string[] skins = skinsString.Split(','); // Sépare la chaîne pour obtenir un tableau de strings

            // Convertit les strings en bool et les assigne à playerStat.possesionId
            playerStat.possesionId = Array.ConvertAll(skins, bool.Parse);



            _inventory.saveCall();

            Debug.Log("Chargement OK");
        }
        Debug.Log("Aucune sauvegarde");
    }

}
