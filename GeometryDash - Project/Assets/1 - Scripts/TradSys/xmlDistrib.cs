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
        texts[0].text = xmlRead.level;
        texts[1].text = xmlRead.quit;
        texts[2].text = xmlRead.option;

        //Quit
        //texts[8].text = xmlRead.quittDem;
        //texts[9].text = xmlRead.oui;
        //texts[10].text = xmlRead.non;

        //texts[14].text = xmlRead.oui;
        //texts[15].text = xmlRead.non;

        ////Next
        //texts[14].text = xmlRead.suivant;
        //texts[18].text = xmlRead.suivant;
        //texts[20].text = xmlRead.suivant;

        //texts[19].text = xmlRead.back;

        ////Beta
        //texts[11].text = xmlRead.beta;
    }
}
