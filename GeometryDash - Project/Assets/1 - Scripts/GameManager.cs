using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
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

    public void OnClickTestLevel()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    //-------------------
    //  METHODES PRIVEE
    //-------------------
}
