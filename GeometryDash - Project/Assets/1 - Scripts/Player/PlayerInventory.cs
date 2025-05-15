using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;

    [Header("Ressources")]
    [SerializeField] SO_PlayerStat SO_playerStats;

    //-------------------
    //  METHODES DEFAULT
    //-------------------

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            AddMoney(1000, "cash");
            AddMoney(1000, "gold");
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
