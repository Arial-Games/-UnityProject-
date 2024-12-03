using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ShopManagement : MonoBehaviour
{
    // Scripts
    [SerializeField] PlayerInventory playerInventory;
    [SerializeField] SO_PlayersSkins[] so_BasicPlayersSkins; //so_TrianglePlayersSkins, so_NavettePlayersSkins, so_RobotsPlayersSkins, so_WheelsPlayersSkins, so_OtherPlayersSkins;

    [Header("Bouttons / Sprite")]
    [SerializeField] GameObject[] buyingButtons;
    [SerializeField] Sprite interoPoint;

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
                buyingButtons[i].GetComponent<Image>().sprite = so_BasicPlayersSkins[i].skinSprite;
            }
            else
            {
                Debug.Log("test");
                if (buyingButtons[i].GetComponent<Image>().sprite = interoPoint)
                {
                    buyingButtons[(i)].GetComponent<Button>().interactable = false;
                }
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


        if (playerInventory.cash < so_BasicPlayersSkins[(id - 1)].price)
        {
            Debug.LogWarning("Argent insufisant !");
        }
        else
        {
            playerInventory.cash -= so_BasicPlayersSkins[(id - 1)].price;
            buyingButtons[(id - 1)].GetComponent<Button>().interactable = false;
            playerInventory.possesionId[(id - 1)] = true;
        }
    }

}
