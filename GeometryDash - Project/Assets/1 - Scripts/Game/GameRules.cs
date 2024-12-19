using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.ParticleSystem;

public class GameRules : MonoBehaviour
{
    [SerializeField] GameObject[] particles;
    [SerializeField] GameObject player;

    //player.SetActive(false); -> desactiver le PlayerController et le stopper
    
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

    public void OnTakeCollectibles()
    {
        // AddScore
        Destroy(Instantiate(particles[2], player.transform.position, transform.rotation), 0.4f);
    }

    public void OnEndLignePass()
    {
        player.SetActive(false);
        // Animation de fin
    }


    //-------------------
    //  METHODES PRIVEE
    //-------------------
}