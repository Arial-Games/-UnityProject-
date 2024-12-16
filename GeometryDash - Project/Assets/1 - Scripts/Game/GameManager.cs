using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //-------------------
    //  METHODES DEFAULT
    //-------------------

    void Start()
    {

    }

    void Update()
    {

    }


    //-------------------
    //  METHODES PUBLIC
    //-------------------

    // Temp DEBUG
    public void OnClickTestLevel()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    //-------------------
    //  METHODES PRIVEE
    //-------------------
}
