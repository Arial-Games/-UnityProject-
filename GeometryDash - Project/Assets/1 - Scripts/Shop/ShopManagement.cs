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
    [SerializeField] SO_PlayersSkins[] so_BasicPlayersSkins; //so_TrianglePlayersSkins, so_NavettePlayersSkins, so_RobotsPlayersSkins, so_WheelsPlayersSkins, so_OtherPlayersSkins;
    [SerializeField] SO_ShopObjects[] so_ShopObjects;

    [Header("Bouttons / Sprite")]
    [SerializeField] GameObject[] buyingButtons;
    [SerializeField] Sprite interoPoint;


    //private int shopTurn = 0;

    // Achat de skins avec suppression et rajout dans inventaire 
    // Ajout auto via SO

    //-------------------
    //  METHODES DEFAULT
    //-------------------

    private void Update()
    {
        for (int i = 0; i < buyingButtons.Length; i++)
        {
            if (i < so_BasicPlayersSkins.Length)
            {
                //shopTurn++;
                buyingButtons[i].GetComponent<Image>().sprite = so_BasicPlayersSkins[i].skinSprite;
            }
            //else if (i < so_ShopObjects.Length + shopTurn)
            //{
            //    buyingButtons[i].GetComponent<Image>().sprite = so_ShopObjects[i].objSprite;
            //}
            else
            {
                if (buyingButtons[i].GetComponent<Image>().sprite = interoPoint)
                {
                    buyingButtons[(i)].GetComponent<Button>().interactable = false;
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


