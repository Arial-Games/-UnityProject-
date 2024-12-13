using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.Animations;
using System.IO;

public class CommunityMenuController : MonoBehaviour
{
    // Références aux panneaux et autres éléments UI
    public GameObject[] communityPanel;


    // Référence à l'écran de chargement
    public GameObject loadingScreen;



    //-------------------
    //  METHODES DEFAULT
    //-------------------

    void Start()
    {
        // Assurez-vous que tous les panneaux sont désactivés au démarrage
        DisableAllPanels();
        communityPanel[0].SetActive(false); // Désactive le panneau communautaire au démarrage
    }

    void Update()
    {
        // Ajoutez ici toute logique supplémentaire nécessaire pendant la mise à jour
    }


    //-------------------
    //  METHODES PUBLIC
    //-------------------

    // Méthode pour activer le panneau communautaire
    public void ActivateCommunityPanel()
    {
        DisableAllPanels();
        communityPanel[0].SetActive(true);
    }

    public void OnClickBackButton()
    {
        communityPanel[0].SetActive(false);
    }

    public void OnClickOpenComm()
    {
        communityPanel[0].SetActive(true);
    }

    // Chargez une scène de manière asynchrone
    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadSceneAsync(sceneIndex));
    }


    //-------------------
    //  METHODES PRIVEE
    //-------------------

    // Coroutine pour charger la scène asynchronement
    private IEnumerator LoadSceneAsync(int sceneIndex)
    {
        loadingScreen.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

        while (!asyncLoad.isDone)
        {
            // Optionnel : Mettez à jour une barre de progression ici
            yield return null;
        }
    }

    // Méthode pour désactiver tous les panneaux
    void DisableAllPanels()
    {
        communityPanel[0].SetActive(false);

    }
}

