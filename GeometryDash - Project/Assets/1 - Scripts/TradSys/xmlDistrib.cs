using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class xmlDistrib : MonoBehaviour
{
    private xmlReader xmlRead;

    //Main scene
    public TextMeshProUGUI[] texts;

    private void Start()
    {
        xmlRead = GameObject.Find("Dictionary").GetComponent<xmlReader>();
    }

    private void Update()
    {
        //texts[0].text = xmlRead.musique;
        //texts[1].text = xmlRead.quality;
        //texts[2].text = xmlRead.language;
        //texts[3].text = xmlRead.back;
        //texts[12].text = xmlRead.back;
        //texts[4].text = xmlRead.fullscreen;

        //Main Menu
        texts[0].text = xmlRead.levelSelect;
        texts[1].text = xmlRead.quit;
        texts[2].text = xmlRead.option;
        texts[3].text = xmlRead.casier;
        texts[4].text = xmlRead.communaute;

        //Quit
        //texts[8].text = xmlRead.quittDem;
        //texts[9].text = xmlRead.oui;
        //texts[10].text = xmlRead.non;

        // Communaute
        texts[10].text = xmlRead.creer;
        texts[11].text = xmlRead.enregistre;
        texts[12].text = xmlRead.recherche;
        texts[13].text = xmlRead.multiJoueurs;
        texts[14].text = xmlRead.scores;

        // Level
        texts[15].text = xmlRead.levelChoice;
        texts[16].text = xmlRead.level + "1";

        // Money
        //texts[17].text = xmlRead.money;
        //texts[18].text = xmlRead.gold;
        //texts[19].text = xmlRead.star;
        //texts[20].text = xmlRead.starCoins;

        // Back button
        texts[5].text = xmlRead.back;
        texts[6].text = xmlRead.back;
        texts[7].text = xmlRead.back;
        texts[8].text = xmlRead.back;
        texts[9].text = xmlRead.back;

        ////Beta
        //texts[11].text = xmlRead.beta;
    }
}
