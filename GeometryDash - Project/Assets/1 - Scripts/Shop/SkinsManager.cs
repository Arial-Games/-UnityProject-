using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinsManager : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] SO_PlayerStat sO_PlayerStat;
    [SerializeField] SO_PlayersSkins[] so_BasicPlayersSkins;

    [Header("Bouttons / Sprite")]
    [SerializeField] GameObject[] skinButton;
    [SerializeField] Sprite interoPoint;
    [SerializeField] GameObject actualSkin;


    private void Start()
    {
        actualSkin.GetComponent<Image>().sprite = so_BasicPlayersSkins[sO_PlayerStat.actualSkinId].skinSprite;
    }

    private void Update()
    {
        for (int i = 0; i < skinButton.Length; i++)
        {
            if (i < so_BasicPlayersSkins.Length)
            {
                skinButton[i].GetComponent<Image>().sprite = so_BasicPlayersSkins[i].skinSprite;

                if (sO_PlayerStat.possesionId[i] == false)
                {
                    skinButton[(i)].GetComponent<Button>().interactable = false;
                }
                else
                {
                    skinButton[(i)].GetComponent<Button>().interactable = true;
                }
            }
            else
            {
                if (skinButton[i].GetComponent<Image>().sprite = interoPoint)
                {
                    skinButton[(i)].GetComponent<Button>().interactable = false;
                }
            }
        }
    }




    //-------------------
    //  METHODES PUBLIC
    //-------------------

    public void onClickSkinButton(int id)
    {
        if (id == 0)
        {
            Debug.LogWarning("Boutton non renseigné");
        }


        if (sO_PlayerStat.possesionId[id - 1] == true)
        {
            sO_PlayerStat.actualSkinId = id - 1;
            actualSkin.GetComponent<Image>().sprite = so_BasicPlayersSkins[id - 1].skinSprite;
        }
    }
}
