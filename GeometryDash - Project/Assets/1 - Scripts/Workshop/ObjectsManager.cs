using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectsManager : MonoBehaviour
{
    [Header("Scripts")]
    /*    [SerializeField] SO_PlayerStat sO_PlayerStat;
        [SerializeField] SO_PlayersSkins[] so_BasicPlayersSkins;
        [SerializeField] SO_ShopObjects[] so_ShopObjects;*/
    [SerializeField] SO_WorkshopObjects[] so_WorkshopObjects;

    [Header("Bouttons / Sprite")]
    [SerializeField] GameObject[] buyingButtons;
    [SerializeField] Sprite interoPoint;

    int levelData = 10;

    //-------------------
    //  METHODES DEFAULT
    //-------------------

    private void Update()
    {
        for (int i = 0; i < buyingButtons.Length; i++)
        {

        }
    }

    private void Start()
    {

    }

    //-------------------
    //  METHODES PUBLIC
    //-------------------

    public void workshopButton(int id)
    {
        if (id == 0)
        {
            Debug.LogWarning("Boutton non renseigné");
        }

        if (so_WorkshopObjects[id].dataSize < levelData)
        {
            Debug.LogWarning("Taille insufisant !");
        }
        else
        {
            levelData -= so_WorkshopObjects[id].dataSize;
            hideButton(id - 1);
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
