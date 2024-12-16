using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.ParticleSystem;

public class GameRules : MonoBehaviour
{
    [SerializeField] GameObject[] particles;
    [SerializeField] GameObject player;


    //-------------------
    //  METHODES DEFAULT
    //-------------------

    void Start()
    {

    }

    void Update()
    {

    }


    //-------------------
    //  METHODES PUBLIC
    //-------------------

    public void OnPlayerDeath()
    {
        // Affichage du panel de retry
        Destroy(Instantiate(particles[0], player.transform.position, transform.rotation), 0.4f);
        player.SetActive(false);
    }


    //-------------------
    //  METHODES PRIVEE
    //-------------------
}