using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUi : MonoBehaviour
{


    //-------------------
    //  METHODES DEFAULT
    //-------------------


    //-------------------
    //  METHODES PUBLIC
    //-------------------

    public void SceneLoadOnBtnClick(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }


    //-------------------
    //  METHODES PRIVEE
    //-------------------

}
