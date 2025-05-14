using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Animations;
using System.IO;
using TMPro;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject[] screenToSwitch;
    [SerializeField] TextMeshProUGUI[] inventoryText;

    [Header("Scripts")]
    [SerializeField] SO_PlayerStat sO_PlayerStat;


    //-------------------
    //  METHODES DEFAULT
    //-------------------

    void Start()
    {

    }

    void Update()
    {
        TextOnDisplay();
        OnEscapeClick();
    }

    //-------------------
    //  METHODES PUBLIC
    //-------------------

    //public void OnClickBackButton()
    //{
    //    screenToSwitch[0].SetActive(false);
    //}

    //public void OnClickOpenShop()
    //{
    //    screenToSwitch[0].SetActive(true);
    //}

    //-------------------
    //  METHODES PRIVEE
    //-------------------

    void OnEscapeClick()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            for (int i = 0; i < screenToSwitch.Length; i++)
            {
                screenToSwitch[i].SetActive(false);
            }
        }
    }

    void TextOnDisplay()
    {
        inventoryText[0].text = "Argent : " + sO_PlayerStat.cash.ToString();
        inventoryText[1].text = "Or : " + sO_PlayerStat.gold.ToString();
        inventoryText[2].text = "Etoile : " + sO_PlayerStat.stars.ToString();
        inventoryText[3].text = "Piece etoiles : " + sO_PlayerStat.starsCoins.ToString();
    }
}
