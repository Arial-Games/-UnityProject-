using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartScene : MonoBehaviour
{
    // Loading Screen
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Image loadingBarFill;
    [SerializeField] TextMeshProUGUI loadingText;


    //-------------------
    //  METHODES PUBLIC
    //-------------------

    public void StartButton(int sceneId)
    {
        StartCoroutine(startSceneAsync(sceneId));
    }


    //-------------------
    //  METHODES PRIVEE
    //-------------------

    IEnumerator startSceneAsync(int placeID)
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;

        AsyncOperation operation = SceneManager.LoadSceneAsync(placeID);
        operation.allowSceneActivation = false;
        loadingScreen.SetActive(true);

        Coroutine loadingTextCoroutine = StartCoroutine(animateLoadingText());

        while (operation.progress < 0.9f)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBarFill.fillAmount = progressValue;

            yield return null;
        }

        yield return new WaitForSeconds(3f);

        operation.allowSceneActivation = true;
        StopCoroutine(loadingTextCoroutine);
    }


    IEnumerator animateLoadingText()
    {
        string baseText = "Loading";
        int dotCount = 0;

        while (true)
        {
            loadingText.text = baseText + new string('.', dotCount + 1);
            dotCount = (dotCount + 1) % 3;
            yield return new WaitForSeconds(0.5f);
        }
    }


}
