using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.UI;

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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if (so_GameHazards.isCollectibles && so_GameHazards.isStars)
            {
                gameRules.OnTakeCollectibles();
                Destroy(gameObject);
            }

            if (so_GameHazards.isGate)
            {
                Debug.Log("Tranformation du Player en " +  so_GameHazards.transformTo);
            }

            if (so_GameHazards.isEndLine)
            {
                Debug.Log("Ligne d'arrivé passé, bien jouer !");
                gameRules.OnEndLignePass();
            }

            if (so_GameHazards.isJumper)
            {
                Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * so_GameHazards.jumpStrenght, ForceMode2D.Impulse);
                //Transform Sprite = collision.gameObject.GetComponent<Transform>();
                //Sprite.Rotate(Vector3.back * 5);

                // Revoir la rotation
            }
        }
    }
}
