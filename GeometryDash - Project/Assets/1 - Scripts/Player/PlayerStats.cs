using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] SO_PlayerStat sO_PlayerStat;
    [SerializeField] SO_PlayersSkins[] so_BasicPlayersSkins;

    [Header("Sprite")]
    [SerializeField] GameObject playerVisu;

    //-------------------
    //  METHODES DEFAULT
    //-------------------

    private void Start()
    {
        playerVisu.GetComponent<SpriteRenderer>().sprite = so_BasicPlayersSkins[sO_PlayerStat.actualSkinId].skinSprite;
    }
}
