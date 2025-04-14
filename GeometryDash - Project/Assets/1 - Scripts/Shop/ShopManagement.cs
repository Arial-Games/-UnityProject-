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

    [SerializeField, Header("NOT USED")] SO_PlayersSkins[] so_WheelsPlayersSkins;

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
        float price = so_BasicPlayersSkins[id - 1].price;

        switch (so_BasicPlayersSkins[id - 1].moneyNeed)
        {
            case SO_PlayersSkins.MoneyNeed.Gold:
                if (sO_PlayerStat.gold < price) Debug.LogWarning("Or insuffisant !");
                else PerformPurchase(id, price, "Gold");
                break;

            case SO_PlayersSkins.MoneyNeed.Argent:
                if (sO_PlayerStat.cash < price) Debug.LogWarning("Argent insuffisant !");
                else PerformPurchase(id, price, "Cash");
                break;

            case SO_PlayersSkins.MoneyNeed.Stars:
                if (sO_PlayerStat.stars < price) Debug.LogWarning("Étoiles insuffisantes !");
                else PerformPurchase(id, price, "Stars");
                break;

            case SO_PlayersSkins.MoneyNeed.StarsCoins:
                if (sO_PlayerStat.starsCoins < price) Debug.LogWarning("StarsCoins insuffisants !");
                else PerformPurchase(id, price, "StarsCoins");
                break;

            default:
                Debug.LogWarning("Monnaie non reconnue !");
                break;
        }
    }

    private void PerformPurchase(int id, float price, string currency)
    {
        switch (currency)
        {
            case "Gold":
                sO_PlayerStat.gold -= price;
                break;
            case "Cash":
                sO_PlayerStat.cash -= price;
                break;
            case "Stars":
                sO_PlayerStat.stars -= price;
                break;
            case "StarsCoins":
                sO_PlayerStat.starsCoins -= price;
                break;
            default:
                Debug.LogWarning("Monnaie non reconnue !");
                return;
        }

        hideButton(id - 1);
        sO_PlayerStat.possesionId[id - 1] = true;
    }


    //-------------------
    //  METHODES PRIVEE
    //-------------------

    void hideButton(int id)
    {
        buyingButtons[(id)].GetComponent<Button>().interactable = false;
    }
}
