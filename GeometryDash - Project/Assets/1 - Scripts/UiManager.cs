using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Animations;
using System.IO;

public class UiManager : MonoBehaviour
{
    [SerializeField] GameObject shopScreen;
    // Les texts

    //-------------------
    //  METHODES DEFAULT
    //-------------------

    void Start()
    {
        shopScreen.SetActive(false);
    }

    void Update()
    {
        
    }

    //-------------------
    //  METHODES PUBLIC
    //-------------------

    public void OnClickBackButton()
    {
        shopScreen.SetActive(false);
    }

    public void OnClickOpenShop()
    {
        shopScreen.SetActive(true);
    }

    //-------------------
    //  METHODES PRIVEE
    //-------------------
}
