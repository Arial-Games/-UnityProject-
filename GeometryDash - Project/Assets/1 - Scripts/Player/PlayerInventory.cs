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
            Debug.LogWarning("CHEAT EN COURS");
        }
    }


    //-------------------
    //  METHODES PUBLIC
    //-------------------

    public void saveCall()
    {
        // Charger les donnés physique si besoin
    }

    public void AddMoney(int ammount, string type)
    {
        if (type == "cash")
            SO_playerStats.cash += ammount;
        else if (type == "gold")
            SO_playerStats.gold += ammount;
    }


    //-------------------
    //  METHODES PRIVEE
    //-------------------
}
