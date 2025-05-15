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
    [SerializeField] SaveData saveData;
    [SerializeField] PlayerInventory playerInventory;

    [SerializeField, Header("Game objects")] GameObject[] particles;
    [SerializeField] GameObject player, gameOverPanel;

    [Header("GUI"), SerializeField] Image[] starsUi;
    public TextMeshProUGUI scoreDisplay, bestScoreDisplay, tryNumberTxt, gameTimerTxt;

    [Header("Audio"), SerializeField] AudioSource deathSound, takeCollectibleSound, onCrossGate;

    [Header("Animation"), SerializeField] Animator deathPanelAnimator;

    // Public
    [Header("Other")] public int playerLevelScore = 0, bestPayerLevelScore = 0, tryNumber = 0;
    [HideInInspector] public float timer = 0f;

    private float incrementInterval = 0.5f;
    private bool isDead = false;
    private Coroutine gameTimerCoroutine;

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

        if (player == null)
            player = GameObject.Find("Basic-player(Clone)");
        RestartGameTimer();
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

    public void OnGateEnter()
    {
        onCrossGate.Play();
    }

    public void OnPlayerDeath()
    {
        tryNumber++;
        deathSound.Play();
        Destroy(Instantiate(particles[0], player.transform.position, transform.rotation), 0.4f);
        player.transform.position = new Vector3(-20, 0, 0);
        timer = 0f;
        RestartGameTimer();
        tryNumberTxt.text = "Essai : " + tryNumber;
    }

    public void OnTakeCollectibles(Transform objPos, bool isStars)
    {
        playerInventory.AddMoney(UnityEngine.Random.Range(0, 20), "cash");
        playerInventory.AddMoney(UnityEngine.Random.Range(0, 5), "gold");
        if (isStars)
            playerLevelScore += 1000;

        saveData.save();

        takeCollectibleSound.Play();
        Vector3 spawnPos = objPos.position + Vector3.down * -0.7f;
        Destroy(Instantiate(particles[2], spawnPos, transform.rotation), 0.4f);
    }

    public void OnEndLignePass()
    {
        GameOver();
    }


    //-------------------
    //  METHODES PRIVEE
    //-------------------

    void GameOver()
    {
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

        deathSound.Play();
        Destroy(Instantiate(particles[0], player.transform.position, transform.rotation), 0.4f);
        player.SetActive(false);
        gameOverPanel.SetActive(true);
        deathPanelAnimator.SetTrigger("Death");
        Cursor.lockState = CursorLockMode.None;

        isDead = true;
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

    private IEnumerator StartGameTimer()
    {
        int timeElapsed = 0;

        while (!isDead)
        {
            int minutes = timeElapsed / 60;
            int seconds = timeElapsed % 60;
            gameTimerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            yield return new WaitForSeconds(1f);
            timeElapsed++;
        }
    }

    public void RestartGameTimer()
    {
        if (gameTimerCoroutine != null)
        {
            StopCoroutine(gameTimerCoroutine);
        }

        gameTimerCoroutine = StartCoroutine(StartGameTimer());
    }
}