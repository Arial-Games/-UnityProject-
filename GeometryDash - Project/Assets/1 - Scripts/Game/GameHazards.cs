using System;
using UnityEngine;
using UnityEngine.UI;

public class GameHazards : MonoBehaviour
{
    [SerializeField] SO_GameHazards so_GameHazards;
    GameRules gameRules;

    GameObject spaceShipVisu;


    //-------------------
    //  METHODES DEFAULT
    //-------------------

    void Start()
    {
        gameRules = GameObject.Find("GameRules").GetComponent<GameRules>();
        spaceShipVisu = GameObject.Find("SpaceShipVisu");
        spaceShipVisu.GetComponent<SpriteRenderer>().enabled = false;
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

    void ToggleSpaceShipVisu(bool isVisible, GameObject playerGO)
    {
        if (spaceShipVisu != null)
        {
            playerGO.GetComponent<PlayerController>().ToggleMode();
            spaceShipVisu.GetComponent<SpriteRenderer>().enabled = isVisible;
        }
    }


    void TrapMoveement()
    {
        if (so_GameHazards.canRotate)
        {
            this.transform.Rotate(Vector3.back * 2);
        }
    }

    void OnPlayerDeathInit()
    {
        gameRules.OnPlayerDeath();
    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (so_GameHazards.isTrap && so_GameHazards.canKill && collision.gameObject.tag == "Player")
        {
            OnPlayerDeathInit();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (so_GameHazards.isCollectibles && so_GameHazards.isStars)
            {
                gameRules.OnTakeCollectibles(this.transform);
                Destroy(gameObject);
            }

            if (so_GameHazards.isGate)
            {
                if (so_GameHazards.transformTo == SO_GameHazards.TransformTo.SpaceShip)
                {
                    ToggleSpaceShipVisu(true, collision.gameObject);
                }
                else if (so_GameHazards.transformTo == SO_GameHazards.TransformTo.Basic)
                {
                    ToggleSpaceShipVisu(false, collision.gameObject);
                }
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
