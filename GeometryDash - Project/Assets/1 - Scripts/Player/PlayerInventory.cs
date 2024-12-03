using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int playerLevel;

    [Header("Ressources")]
    public int gold;
    public int cash;
    public int stars;
    public int starsCoins;

    [Header("Other")]
    public bool[] possesionId;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            gold += 100;
            cash += 100;
            stars += 100;
            starsCoins += 100;
            Debug.LogError("CHEAT EN COURS");
        }
    }
}
