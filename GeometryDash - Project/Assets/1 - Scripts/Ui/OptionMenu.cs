using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Animations;
using System.IO;
using UnityEditor;
using TMPro;

public class OptionMenu : MonoBehaviour
{
    public GameObject optionGlobalPanel;
    public GameObject[] optionPanel;

    [SerializeField] SaveData saveData;

    [SerializeField] bool mainMenu = false, isMute = false;

    // Audio
    [SerializeField, Header("Sound")] private AudioMixer audioMixer;


    // Sensivity
    bool invertY = false;
    float horizontalSensitivity = 2f;
    float verticalSensitivity = 2f;


    // Input
    [SerializeField, Header("Input")] Button changeJumpButton;
    [SerializeField] TextMeshProUGUI buttonText;
    bool waitingForInput = false;

    [SerializeField] Slider[] soundSlider;

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


        // Gestion des Inputs
        if (waitingForInput)
        {
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(key))
                {
                    buttonText.text = "Saut avec : " + key.ToString();
                    waitingForInput = false;
                    changeJumpButton.interactable = true;
                    Debug.Log("Nouvelle touche de saut : " + key.ToString());
                    break;
                }
            }
        }
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

    public void OnChangeJumpKeyClick()
    {
        changeJumpButton.interactable = false;
        buttonText.text = "Appuyez sur une touche...";
        waitingForInput = true;
    }

    #region Volume
    public void SetMasterVolume(float volume)
    {
        if (isMute == false)
        {
            audioMixer.SetFloat("MasterVolume", volume);
        }
    }

    public void SetEffectVolume(float volume)
    {
        if (isMute == false)
        {
            audioMixer.SetFloat("EffectVolume", volume);
        }
    }

    public void SetMusicVolume(float volume)
    {
        if (isMute == false)
        {
            audioMixer.SetFloat("MusicVolume", volume);
        }
    }

    public void MuteAllSound(bool _isMute)
    {
        isMute = _isMute;

        float musicVolume = isMute ? -80 : -15;
        float effectVolume = isMute ? -80 : 0;
        float masterVolume = isMute ? -80 : 0;

        audioMixer.SetFloat("MusicVolume", musicVolume);
        audioMixer.SetFloat("EffectVolume", effectVolume);
        audioMixer.SetFloat("MasterVolume", masterVolume);

        for (int i = 0; i < soundSlider.Length; i++)
        {
            soundSlider[i].value = isMute ? -80 : (i == 0 ? -15 : 0);
        }
    }
    #endregion

    #region Souris Clavier
    public void ToggleInvertY(bool state)
    {
        invertY = state;
    }

    public void UpdateHorizontalSensitivity(float value)
    {
        horizontalSensitivity = value;
    }

    public void UpdateVerticalSensitivity(float value)
    {
        verticalSensitivity = value;
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
