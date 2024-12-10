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
    public GameObject communityGlobalPanel;


    // Référence à l'écran de chargement
    public GameObject loadingScreen;

    void Start()
    {
        // Assurez-vous que tous les panneaux sont désactivés au démarrage
        DisableAllPanels();
        communityGlobalPanel.SetActive(false); // Désactive le panneau communautaire au démarrage
    }

    void Update()
    {
        // Ajoutez ici toute logique supplémentaire nécessaire pendant la mise à jour
    }

    // Méthode pour activer le panneau communautaire
    public void ActivateCommunityPanel()
    {
        DisableAllPanels();
        communityGlobalPanel.SetActive(true);
    }

    // Méthode pour désactiver tous les panneaux
    private void DisableAllPanels()
    {
        communityGlobalPanel.SetActive(false);
       
    }
    
       public void OnClickBackButton()
    {
        communityGlobalPanel.SetActive(false);
    }

    public void OnClickOpenComm()
    {
        communityGlobalPanel.SetActive(true);
    }


    // Chargez une scène de manière asynchrone
    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadSceneAsync(sceneIndex));
    }

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
}

