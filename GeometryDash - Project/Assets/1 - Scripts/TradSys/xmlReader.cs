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

    [HideInInspector] public string musique, quality, language, back, fullscreen, levelSelect, quit, level, option, suivant, quittDem, oui, non, _menuVeri, beta, galerie;

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
                if(value.Name == "Name")
                {
                    obj.Add(value.Name, value.InnerText);
                }

                //Parametres
                if (value.Name == "Musique")
                {
                    obj.Add(value.Name, value.InnerText);
                }

                if (value.Name == "Qualite")
                {
                    obj.Add(value.Name, value.InnerText);
                }

                if (value.Name == "Langue")
                {
                    obj.Add(value.Name, value.InnerText);
                }

                if (value.Name == "Back")
                {
                    obj.Add(value.Name, value.InnerText);
                }

                if (value.Name == "Fullscreen")
                {
                    obj.Add(value.Name, value.InnerText);
                }

                //MainMenu Menu
                if (value.Name == "LevelSelect")
                {
                    obj.Add(value.Name, value.InnerText);
                }

                if (value.Name == "Quit")
                {
                    obj.Add(value.Name, value.InnerText);
                }

                if (value.Name == "Option")
                {
                    obj.Add(value.Name, value.InnerText);
                }

                if (value.Name == "Level")
                {
                    obj.Add(value.Name, value.InnerText);
                }

                if (value.Name == "Suivant")
                {
                    obj.Add(value.Name, value.InnerText);
                }

                if (value.Name == "QuitVeri")
                {
                    obj.Add(value.Name, value.InnerText);
                }

                if (value.Name == "Oui")
                {
                    obj.Add(value.Name, value.InnerText);
                }

                if (value.Name == "Non")
                {
                    obj.Add(value.Name, value.InnerText);
                }

                if (value.Name == "MenuVeri")
                {
                    obj.Add(value.Name, value.InnerText);
                }

                if (value.Name == "PasDispo")
                {
                    obj.Add(value.Name, value.InnerText);
                }

                if (value.Name == "Galerie")
                {
                    obj.Add(value.Name, value.InnerText);
                }
            }
            languages.Add(obj);
        }
    }

    public void ValueChangeCheck()
    {
        currentLangue = selectDropdown.value;
    }
}
