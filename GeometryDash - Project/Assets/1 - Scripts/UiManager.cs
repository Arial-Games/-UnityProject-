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
    [SerializeField] GameObject shopScreen;
    [SerializeField] TextMeshProUGUI[] inventoryText;

    [Header("Scripts")]
    [SerializeField] PlayerInventory playerInventory;


    //-------------------
    //  METHODES DEFAULT
    //-------------------

    void Start()
    {
        shopScreen.SetActive(false);

        // Placement à revoir
        textOnDisplay();
    }

    void Update()
    {

    }

    //-------------------
    //  METHODES PUBLIC
    //-------------------

    public void OnClickBackButton()
    {
        shopScreen.SetActive(false);
    }

    public void OnClickOpenShop()
    {
        shopScreen.SetActive(true);
    }

    //-------------------
    //  METHODES PRIVEE
    //-------------------

    void textOnDisplay()
    {
        inventoryText[0].text = "Argent : " +  playerInventory.gold.ToString();
        inventoryText[1].text = "Or : " + playerInventory.cash.ToString();
        inventoryText[2].text = "Etoile : " + playerInventory.stars.ToString();
        inventoryText[3].text = "Piece etoiles : " + playerInventory.starsCoins.ToString();
    }
}
