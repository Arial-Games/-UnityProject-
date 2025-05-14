using System;
using UnityEngine;
using UnityEngine.UI;
using PowerOrbEffects = SO_GameHazards.PowerOrbEffects;
public class GameHazards : MonoBehaviour
{
    [SerializeField] SO_GameHazards so_GameHazards;
    GameRules gameRules;

    GameObject[] playerVisu = new GameObject[4];


    //-------------------
    //  METHODES DEFAULT
    //-------------------

    void Start()
    {
        gameRules = GameObject.Find("GameRules").GetComponent<GameRules>();
        playerVisu[0] = GameObject.Find("PlayerVisual");
        playerVisu[1] = GameObject.Find("SpaceShipVisu");
        playerVisu[2] = GameObject.Find("WheelsVisu");
        playerVisu[3] = GameObject.Find("TriangleVisu");

        DisableAllVisu();
        playerVisu[0].GetComponent<SpriteRenderer>().enabled = true;
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

    void DisableAllVisu()
    {
        if (playerVisu[0] != null)
        {
            for (int i = 0; i < playerVisu.Length; i++)
            {
                if (playerVisu[i]?.GetComponent<SpriteRenderer>() is SpriteRenderer sr)
                {
                    sr.enabled = false;
                }
                else
                {
                    Debug.LogError($"playerVisu[{i}] est null ou pas de Rendere");
                }
            }
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
            DisableAllVisu();
            playerVisu[0].GetComponent<SpriteRenderer>().enabled = true;
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

         if (so_GameHazards.isCollectibles && so_GameHazards.isPowerUp)
        {
            var powerUpManager = collision.GetComponent<PowerUpBehaviour>();
            if (powerUpManager != null)
            {
                string effectType = so_GameHazards.powerOrbEffects switch
                {
                    PowerOrbEffects.Speed => "speed_2x",
                    PowerOrbEffects.Invincibility => "invincible",
                    PowerOrbEffects.Jump => "jump_2x",
                    _ => "none"
                };
                
                if (effectType != "none")
                {
                    powerUpManager.ActivatePowerUp(effectType, so_GameHazards.powerUpDuration);
                    gameRules.OnTakeCollectibles(this.transform);
                    Destroy(gameObject);
                }
            }
        }

            if (so_GameHazards.isGate)
            {
                int modeID = 0;
                DisableAllVisu();
                if (so_GameHazards.transformTo == SO_GameHazards.TransformTo.Basic)
                {
                    playerVisu[0].GetComponent<SpriteRenderer>().enabled = true;
                    modeID = 1;
                }
                else if (so_GameHazards.transformTo == SO_GameHazards.TransformTo.SpaceShip)
                {
                    playerVisu[0].GetComponent<SpriteRenderer>().enabled = true;
                    playerVisu[1].GetComponent<SpriteRenderer>().enabled = true;
                    modeID = 2;
                }
                else if (so_GameHazards.transformTo == SO_GameHazards.TransformTo.Wheels)
                {
                    playerVisu[2].GetComponent<SpriteRenderer>().enabled = true;
                    modeID = 3;
                }
                else if (so_GameHazards.transformTo == SO_GameHazards.TransformTo.Triangle)
                {
                    playerVisu[3].GetComponent<SpriteRenderer>().enabled = true;
                    modeID = 4;
                }
                else if (so_GameHazards.transformTo == SO_GameHazards.TransformTo.Navet)
                {
                    modeID = 5;
                }
                else if (so_GameHazards.transformTo == SO_GameHazards.TransformTo.Robot)
                {
                    modeID = 6;
                }
                collision.gameObject.GetComponent<PlayerController>().ToggleMode(modeID);
            }
        }

        if (so_GameHazards.isEndLine)
        {
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
