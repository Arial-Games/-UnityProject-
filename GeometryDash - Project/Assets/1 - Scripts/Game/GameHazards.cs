using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class GameHazards : MonoBehaviour
{
    [SerializeField] SO_GameHazards so_GameHazards;
    [SerializeField] GameObject particles;


    //-------------------
    //  METHODES DEFAULT
    //-------------------

    void Start()
    {

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
            Destroy(Instantiate(particles, collision.gameObject.transform.position, transform.rotation), 0.4f);
            collision.gameObject.SetActive(false);
        }
    }
}
