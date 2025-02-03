using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ShopManagement : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] SO_PlayerStat sO_PlayerStat;
    [SerializeField] SO_PlayersSkins[] so_BasicPlayersSkins;
    [SerializeField] SO_ShopObjects[] so_ShopObjects;

    [Header("Bouttons / Sprite")]
    [SerializeField] GameObject[] buyingButtons;
    [SerializeField] Sprite interoPoint;

    //-------------------
    //  METHODES DEFAULT
    //-------------------

    private void Update()
    {
        for (int i = 0; i < buyingButtons.Length; i++)
        {
            if (i < so_BasicPlayersSkins.Length)
            {
                // Get the "SkinVisu" child and set its sprite
                Image skinImage = buyingButtons[i].transform.Find("SkinVisu").GetComponent<Image>();
                skinImage.sprite = so_BasicPlayersSkins[i].skinSprite;

                // Get the "PriceText" child and set its price
                TextMeshProUGUI priceText = buyingButtons[i].transform.Find("PriceText (TMP)").GetComponent<TextMeshProUGUI>();
                priceText.text = so_BasicPlayersSkins[i].price.ToString() + " " + so_BasicPlayersSkins[i].moneyNeed;
            }
            else
            {
                Image skinImage = buyingButtons[i].transform.Find("SkinVisu").GetComponent<Image>();
                if (skinImage.sprite == interoPoint)
                {
                    buyingButtons[i].GetComponent<Button>().interactable = false;
                }
            }
        }
    }

    private void Start()
    {
        for (int i = 0; i < sO_PlayerStat.possesionId.Length; i++)
        {
            if (sO_PlayerStat.possesionId[i])
            {
                hideButton(i);
            }
        }
    }

    //-------------------
    //  METHODES PUBLIC
    //-------------------

    public void shopButton(int id)
    {
        if (id == 0)
        {
            Debug.LogWarning("Boutton non renseigné");
        }

        if (sO_PlayerStat.cash < so_BasicPlayersSkins[(id - 1)].price)
        {
            Debug.LogWarning("Argent insufisant !");
        }
        else
        {
            sO_PlayerStat.cash -= so_BasicPlayersSkins[(id - 1)].price;
            hideButton(id - 1);
            sO_PlayerStat.possesionId[(id - 1)] = true;
        }
    }

    //-------------------
    //  METHODES PRIVEE
    //-------------------

    void hideButton(int id)
    {
        buyingButtons[(id)].GetComponent<Button>().interactable = false;
    }
}
