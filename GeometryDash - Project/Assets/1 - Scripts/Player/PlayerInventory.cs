using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Ressources")]
    [SerializeField] SO_PlayerStat SO_playerStats;

    //-------------------
    //  METHODES DEFAULT
    //-------------------

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            SO_playerStats.gold += 100;
            SO_playerStats.cash += 200;
            SO_playerStats.stars += 300;
            SO_playerStats.starsCoins += 400;
            Debug.LogError("CHEAT EN COURS");
        }
    }


    //-------------------
    //  METHODES PUBLIC
    //-------------------

    public void saveCall()
    {
        // Charger les donnés physique si besoin
    }


    //-------------------
    //  METHODES PRIVEE
    //-------------------
}
