using System;
using UnityEngine;
using UnityEngine.UI;

public class GameHazards : MonoBehaviour
{
    [SerializeField] SO_GameHazards so_GameHazards;
    [SerializeField] GameRules gameRules;

    GameObject[] playerVisu = new GameObject[4];


    //-------------------
    //  METHODES DEFAULT
    //-------------------

    void Start()
    {
        gameRules = GameObject.Find("GameRules").GetComponent<GameRules>();
        if (gameRules == null)
        {
            gameRules = FindObjectOfType<GameRules>();
            if (gameRules == null)
            {
                Debug.LogError("GameRules non trouvé! Assurez-vous qu'il existe dans la scène.");
            }
        }
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
        if (gameRules == null)
        {
            Debug.LogError("gameRules est null dans OnPlayerDeathInit!");
            return;
        }
        Debug.Log("Appel de OnPlayerDeath sur gameRules");
        gameRules.OnPlayerDeath();

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"SO_GameHazards - isTrap: {so_GameHazards.isTrap}, canKill: {so_GameHazards.canKill}");
        Debug.Log($"Collision Tag: {collision.gameObject.tag}");

        if (so_GameHazards.isTrap && so_GameHazards.canKill && collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Conditions de mort remplies - Appel de OnPlayerDeathInit()");
            DisableAllVisu();
            playerVisu[0].GetComponent<SpriteRenderer>().enabled = true;
            OnPlayerDeathInit();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        Debug.Log($"→ touché {name} / type:{so_GameHazards.name}");
        Debug.Log("Touché : " + collision.name);   // doit apparaître
        if (collision.gameObject.tag == "Player")
        {
            if (so_GameHazards.isCollectibles && so_GameHazards.isStars)
            {
                gameRules.OnTakeCollectibles(this.transform);
                Destroy(gameObject);
            }

            if (so_GameHazards.isCollectibles && so_GameHazards.isPowerUp)
            {
                gameRules.OnTakeCollectibles(this.transform);
                Destroy(gameObject);
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
    var rb = collision.attachedRigidbody;   // prend le RB même si sur le parent
    if (!rb) return;

    rb.velocity = Vector2.zero;                         // reset Y
    rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse); // 25 comme ton Jump()
}

    }
}