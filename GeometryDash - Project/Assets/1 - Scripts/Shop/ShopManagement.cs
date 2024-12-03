using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopManagement : MonoBehaviour
{
    // Scripts
    PlayerInventory playerInventory;
    [SerializeField] SO_PlayersSkins[] so_BasicPlayersSkins, so_TrianglePlayersSkins, so_NavettePlayersSkins, so_RobotsPlayersSkins, so_WheelsPlayersSkins, so_OtherPlayersSkins;


    public GameObject[] buyingButtons;

    // Achat de skins avec suppression et rajout dans inventaire 
    // Ajout auto via SO

    private void Update()
    {
        for (int i = 0; i < buyingButtons.Length; i++)
        {
            buyingButtons[i].GetComponent<Image>().sprite = so_BasicPlayersSkins[i].skinSprite;
        }
    }

}
