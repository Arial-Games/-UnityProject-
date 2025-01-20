using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playerGO;

    //-------------------
    //  METHODES DEFAULT
    //-------------------

    void Start()
    {
        //Time.timeScale = 0.5f;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        if (playerGO.transform.position == new Vector3(playerGO.transform.position.x, -5, playerGO.transform.position.z))
        {
            // Die moment
            Destroy(playerGO);
        }
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
