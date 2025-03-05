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

    [SerializeField] SaveData saveData;

    [SerializeField] bool mainMenu = false;

    // Loading Screen
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Image loadingBarFill;

    // Audio
    [SerializeField, Header("Sound")] private AudioMixer audioMixer;


    //-------------------
    //  METHODES DEFAULT
    //-------------------

    int actOptionPanel = 0;

    void Start()
    {
        DiableAllPanel();

        Screen.SetResolution(1920, 1080, Screen.fullScreen);
        Screen.fullScreen = true;
    }

    private void Update()
    {
        OnInputWindowsChange();
    }


    //-------------------
    //  METHODES PUBLIC
    //-------------------


    public void OnButtonWindowsChange(int winId)
    {
        if (mainMenu == true)
        {
            DiableAllPanel();
            optionGlobalPanel.SetActive(true);
            optionPanel[winId].SetActive(true);
        }
    }

    public void StartButton(int sceneId)
    {
        StartCoroutine(startSceneAsync(sceneId));
    }


    public void OptionButton()
    {
        WindowsMove();
        mainMenu = true;
    }

    public void BackButton()
    {
        saveData.save();
        mainMenu = false;
        DiableAllPanel();
    }

    public void LeaveGame()
    {
        saveData.save();
        Application.Quit();
    }



    #region OptionZone
    public void SetQuality(int qualityInd)
    {
        QualitySettings.SetQualityLevel(qualityInd);
    }

    #region Volume
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetEffectVolume(float volume)
    {
        audioMixer.SetFloat("EffectVolume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }
    #endregion

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


    //-------------------
    //  METHODES PRIVEE
    //-------------------

    void OnEscapeClick()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            DiableAllPanel();
        }
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

    void OnInputWindowsChange()
    {
        if (mainMenu == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                mainMenu = false;
                DiableAllPanel();

            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                actOptionPanel++;
                WindowsMove();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                actOptionPanel--;
                WindowsMove();
            }
        }
    }


    void WindowsMove()
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

        DiableAllPanel();
        optionGlobalPanel.SetActive(true);
        optionPanel[actOptionPanel].SetActive(true);
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


    void DiableAllPanel()
    {
        optionGlobalPanel.SetActive(false);
        for (int i = 0; i < optionPanel.Length; i++)
        {
            optionPanel[i].SetActive(false);
        }

    }
}
