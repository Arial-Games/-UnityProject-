using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelUi : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gameTimer;
    [SerializeField] GameObject pauseMenu;
    public bool isDead = false, isPaused = false;

    //-------------------
    //  METHODES DEFAULT
    //-------------------

    private void Start()
    {
        pauseMenu.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(StartGameTimer());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    //-------------------
    //  METHODES PUBLIC
    //-------------------

    public void TogglePauseMenu()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }


    //-------------------
    //  METHODES PRIVEE
    //-------------------

    private IEnumerator StartGameTimer()
    {
        int timeElapsed = 0;

        while (isDead == false)
        {
            int minutes = timeElapsed / 60;
            int seconds = timeElapsed % 60;
            gameTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            yield return new WaitForSeconds(1f);
            timeElapsed++;
        }
    }


}
