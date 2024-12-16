using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayersScore : MonoBehaviour
{
    [SerializeField] GameObject[] playerScoreGO;
    [SerializeField] SO_PlayersSkins[] so_skins; // TEMP - Rendre sa synamique plus tard


    //-------------------
    //  METHODES DEFAULT
    //-------------------

    void Start()
    {
        for (int i = 0; i < playerScoreGO.Length; i++)
        {
            playerScoreGO[i].GetComponentInChildren<TextMeshProUGUI>().text = so_skins[i].publicName;
            playerScoreGO[i].GetComponentInChildren<TextMeshProUGUI>().color = so_skins[i].mainColor;
            Image imageComponent = playerScoreGO[i].transform.Find("PlayerPP")?.GetComponent<Image>();
            if (imageComponent != null)
            {
                imageComponent.sprite = so_skins[i].skinSprite;
            }
        }
    }

    void Update()
    {

    }


    //-------------------
    //  METHODES PUBLIC
    //-------------------




    //-------------------
    //  METHODES PRIVEE
    //-------------------

}
