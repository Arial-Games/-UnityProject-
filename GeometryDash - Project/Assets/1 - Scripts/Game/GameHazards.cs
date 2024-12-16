using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class GameHazards : MonoBehaviour
{
    [SerializeField] SO_GameHazards so_GameHazards;
    GameRules gameRules;


    //-------------------
    //  METHODES DEFAULT
    //-------------------

    void Start()
    {
        gameRules = GameObject.Find("GameRules").GetComponent<GameRules>();
    }

    void Update()
    {
        TrapMoveement();
    }


    //-------------------
    //  METHODES PUBLIC
    //-------------------




    //-------------------
    //  METHODES PRIVEE
    //-------------------

    void TrapMoveement()
    {
        if (so_GameHazards.canRotate)
        {
            this.transform.Rotate(Vector3.back * 2);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (so_GameHazards.isTrap && so_GameHazards.canKill && collision.gameObject.tag == "Player")
        {
            gameRules.OnPlayerDeath();
        }
    }
}
