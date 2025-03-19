using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    // Loading Screen
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Image loadingBarFill;


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
        AsyncOperation operation = SceneManager.LoadSceneAsync(placeID);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBarFill.fillAmount = progressValue;

            yield return null;
        }
    }
}
