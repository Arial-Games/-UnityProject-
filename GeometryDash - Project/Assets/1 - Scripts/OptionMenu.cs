using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Animations;
using System.IO;

public class OptionMenu : MonoBehaviour
{
    public GameObject optionGlobalPanel;
    public GameObject[] optionPanel;
    public AudioMixer[] audioMixerMusic;

    [SerializeField] bool mainMenu = false;


    //public SO_PlayerStat playerStat;


    // Loading Screen

    [SerializeField] GameObject loadingScreen;
    [SerializeField] Image loadingBarFill;



    int actOptionPanel = 0;

    void Start()
    {
        diableAllPanel();

        Screen.SetResolution(1920, 1080, Screen.fullScreen);
        Screen.fullScreen = true;
    }

    private void Update()
    {
        if (mainMenu == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                mainMenu = false;
                diableAllPanel();

            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                actOptionPanel++;
                windowsMove();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                actOptionPanel--;
                windowsMove();
            }
        }



    }


    void windowsMove()
    {
        // Boucle
        if (actOptionPanel <= -1)
        {
            actOptionPanel = 5;
        }

        if (actOptionPanel >= 6)
        {
            actOptionPanel = 0;
        }

        diableAllPanel();
        optionGlobalPanel.SetActive(true);
        optionPanel[actOptionPanel].SetActive(true);
    }



    public void startButton()
    {
        StartCoroutine(startSceneAsync(1));
    }
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

    // Lancemenet de la partie sur la save 
/*
    public void loadButton()
    {
        string saveSeparator = "%DATA%";

        if (File.Exists(Application.dataPath + "/data.txt"))
        {
            string saveString = File.ReadAllText(Application.dataPath + "/data.txt");


            string[] content = saveString.Split(new[] { saveSeparator }, System.StringSplitOptions.None);
            playerStat.money = float.Parse(content[0]);
            playerStat.sharpMetal = float.Parse(content[1]);
            playerStat.rutsyMetal = float.Parse(content[2]);

            playerStat.XP = int.Parse(content[3]);
            playerStat.storyProgression = int.Parse(content[4]);
            playerStat._actTiers = int.Parse(content[5]);
            playerStat._actHouseTiers = int.Parse(content[6]);


            playerStat.openSave = bool.Parse(content[7]);
            playerStat.openSave = true;
            StartCoroutine(loadSceneAsync(1));

        }
    }
    IEnumerator loadSceneAsync(int placeID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(placeID);
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBarFill.fillAmount = progressValue;

            yield return null;
        }
    }*/


    public void optionButton()
    {
        windowsMove();
        mainMenu = true;
    }

    public void backButton()
    {
        mainMenu = false;
        diableAllPanel();
    }

    public void leaveGame()
    {
        Application.Quit();
    }



    #region OptionZone
    public void SetQuality(int qualityInd)
    {
        QualitySettings.SetQualityLevel(qualityInd);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixerMusic[0].SetFloat("MusicVolume", volume);
        audioMixerMusic[1].SetFloat("MusicVolume", volume);
    }

    public void FullScreen(bool isFullScreen)
    {
        if (isFullScreen == false)
        {
            Screen.fullScreen = false;
        }
        else
            Screen.fullScreen = true;
    }
    #endregion


    void diableAllPanel()
    {
        optionGlobalPanel.SetActive(false);
        for (int i = 0; i < optionPanel.Length; i++)
        {
            optionPanel[i].SetActive(false);
        }

    }
}
