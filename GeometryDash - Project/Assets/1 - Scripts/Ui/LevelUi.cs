using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelUi : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gameTimer;
    public bool isDead = false;

    //-------------------
    //  METHODES DEFAULT
    //-------------------

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(StartGameTimer());
    }


    //-------------------
    //  METHODES PUBLIC
    //-------------------



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
