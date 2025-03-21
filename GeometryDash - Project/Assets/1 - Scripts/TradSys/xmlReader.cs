using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Text;
using UnityEngine.UI;
using TMPro;

public class xmlReader : MonoBehaviour
{
    public TextAsset dictio;
    public string langueName;
    public int currentLangue;

    [HideInInspector] public string musique, quality, language, back, fullscreen, levelSelect, quit, level, option, suivant, quittDem, oui, non, _menuVeri, beta, galerie, magasin, casier, creer, enregistre, recherche, multiJoueurs, scores;

    public TMP_Dropdown selectDropdown;

    List<Dictionary<string, string>> languages = new List<Dictionary<string, string>>();
    Dictionary<string, string> obj;

    private void Awake()
    {
        Reader();
    }

    private void Update()
    {
        languages[currentLangue].TryGetValue("Name", out langueName);
        //Parametres
        languages[currentLangue].TryGetValue("Musique", out musique);
        languages[currentLangue].TryGetValue("Qualite", out quality);
        languages[currentLangue].TryGetValue("Langue", out language);
        languages[currentLangue].TryGetValue("Back", out back);
        languages[currentLangue].TryGetValue("Fullscreen", out fullscreen);

        //Main Menu 
        languages[currentLangue].TryGetValue("LevelSelect", out levelSelect);
        languages[currentLangue].TryGetValue("Quit", out quit);
        languages[currentLangue].TryGetValue("Option", out option);
        languages[currentLangue].TryGetValue("Level", out level);
        languages[currentLangue].TryGetValue("Suivant", out suivant);
        languages[currentLangue].TryGetValue("Galerie", out galerie);

        //Quit
        languages[currentLangue].TryGetValue("QuitVeri", out quittDem);
        languages[currentLangue].TryGetValue("Oui", out oui);
        languages[currentLangue].TryGetValue("Non", out non);
        languages[currentLangue].TryGetValue("MenuVeri", out _menuVeri);
        languages[currentLangue].TryGetValue("PasDispo", out beta);

        languages[currentLangue].TryGetValue("Magasin", out magasin);
        languages[currentLangue].TryGetValue("Casier", out casier);
        languages[currentLangue].TryGetValue("Creer", out creer);
        languages[currentLangue].TryGetValue("Enregistre", out enregistre);
        languages[currentLangue].TryGetValue("Recherche", out recherche);
        languages[currentLangue].TryGetValue("MultiJoueurs", out multiJoueurs);
        languages[currentLangue].TryGetValue("Scores", out scores);
    }

    void Reader()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(dictio.text);
        XmlNodeList lanuageList = xmlDoc.GetElementsByTagName("language");

        foreach (XmlNode languageValue in lanuageList)
        {
            XmlNodeList languagueContent = languageValue.ChildNodes;
            obj = new Dictionary<string, string>();

            foreach (XmlNode value in languagueContent)
            {
                obj.Add(value.Name, value.InnerText);
            }
            languages.Add(obj);
        }
    }

    public void ValueChangeCheck()
    {
        currentLangue = selectDropdown.value;
    }
}
