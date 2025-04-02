using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.ParticleSystem;
using TMPro;
using UnityEngine.UI;

public class GameRules : MonoBehaviour
{
    [SerializeField, Header("Script")] LevelSaveData levelSaveData;

    [SerializeField] GameObject[] particles;
    [SerializeField] GameObject player, gameOverPanel;

    [SerializeField] Image[] starsUi;
    public TextMeshProUGUI scoreDisplay, bestScoreDisplay;

    // Public
    public int playerLevelScore = 0, bestPayerLevelScore = 0;
    [HideInInspector] public float timer = 0f;

    private float incrementInterval = 0.5f;
    private bool isDead = false;


    //player.SetActive(false); -> desactiver le PlayerController et le stopper

    //-------------------
    //  METHODES DEFAULT
    //-------------------

    void Start()
    {
        gameOverPanel.SetActive(false);
        //bestScoreDisplay.text = "Meilleur score : " + bestPayerLevelScore;

        for (int i = 0; i < starsUi.Length; i++)
        {
            starsUi[i].enabled = false;
        }

        player = GameObject.Find("Basic-player(Clone)");
    }

    void Update()
    {
        scoreDisplay.text = "Score : " + playerLevelScore;

        //if (playerLevelScore > bestPayerLevelScore)
        //{
        //    bestPayerLevelScore = playerLevelScore;
        //    bestScoreDisplay.text = "Meilleur score : " + bestPayerLevelScore;
        //}


        if (isDead == false)
        {
            OnSecondChange();
        }
    }


    //-------------------
    //  METHODES PUBLIC
    //-------------------

    public void OnPlayerDeath()
    {
        Destroy(Instantiate(particles[0], player.transform.position, transform.rotation), 0.4f);
        player.SetActive(false);

        levelSaveData.ApplyAndSaveBestScore();

        // GUI
        if (playerLevelScore > 1000)
        {
            starsUi[0].enabled = true;
        }
        else if (playerLevelScore > 2500)
        {
            starsUi[1].enabled = true;
        }
        else if (playerLevelScore > 5000)
        {
            starsUi[2].enabled = true;
        }

        gameOverPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;

        isDead = true;
    }

    public void OnTakeCollectibles(Transform objPos)
    {
        playerLevelScore += 1000;
        Vector3 spawnPos = objPos.position + Vector3.down * -0.2f;
        Destroy(Instantiate(particles[2], spawnPos, transform.rotation), 0.4f);

    }

    void OnSecondChange()
    {
        timer += Time.deltaTime;

        if (timer >= incrementInterval)
        {
            timer = 0f;
            playerLevelScore++;
        }
    }

    public void OnEndLignePass()
    {
        Destroy(Instantiate(particles[0], player.transform.position, transform.rotation), 0.4f);
        player.SetActive(false);
        gameOverPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;

        isDead = true;
    }


    //-------------------
    //  METHODES PRIVEE
    //-------------------
}