using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using static UnityEngine.Debug;
using System.Collections;

using UnityEngine.EventSystems;

public class LevelEditor : MonoBehaviour
{
    [SerializeField] private GameObject sceneRoot;           // Le conteneur (LevelRoot) pour les objets de la scène
    [SerializeField] private Transform paletteContainer;     // Le conteneur UI pour les icônes d'objets (ex: ObjectPlacer - Panel)
    [SerializeField] private GameObject paletteButtonPrefab; // Le prefab du bouton d'icône à instancier pour chaque objet
    [SerializeField] private SO_WorkshopObjects[] so_WorkshopObjects;
        private SO_WorkshopObjects currentSO = null;
        private int currentFileIndex = 1;
 // Flag pour autoriser ou non les modifications
    private bool isEditing = true;

    private List<Placeable> placed = new List<Placeable>(); 
     void Start()
    {
        Debug.Log($"▶ LevelEditor.Start() – so_WorkshopObjects.Length = {so_WorkshopObjects.Length}");
        Debug.Log($"▶ paletteButtonPrefab = {paletteButtonPrefab}");
        Debug.Log($"▶ paletteContainer = {paletteContainer}");

        for (int i = 0; i < so_WorkshopObjects.Length; i++)
        {
       SO_WorkshopObjects so = so_WorkshopObjects[i];
            Debug.Log($" • Création bouton pour « {so.typeName} »");

            if (paletteButtonPrefab == null || paletteContainer == null)
            {
                Debug.LogError("❌ prefab ou container non assigné !");
                return;
            }

            GameObject btnObj = Instantiate(paletteButtonPrefab, paletteContainer);
            btnObj.name = $"Btn_{so.typeName}";
            var img = btnObj.GetComponent<Image>();
            if (img != null) img.sprite = so.objSprite;

            var btn = btnObj.GetComponent<Button>();
            if (btn == null)
            {
                Debug.LogError($"❌ Pas de Button sur {btnObj.name}");
                continue;
            }
            
            int index = i;
            btn.onClick.AddListener(() => SelectObject(index));


        }
    }
public void SelectObject(int i)
    {
        if (i >= 0 && i < so_WorkshopObjects.Length)
        {
            currentSO = so_WorkshopObjects[i];
            Debug.Log($"▶ Objet sélectionné: {currentSO.typeName}");
        }
    }

    public void ButtonData(int i )
    {
        StartCoroutine(Fade(i));
    }


    IEnumerator Fade(int i)
    {
        SO_WorkshopObjects so = so_WorkshopObjects[i];
        //selectedObject = so_WorkshopObjects[realId];

        Debug.Log($"▶ Fade()++++++++++ {so}");
        yield return new WaitForSeconds(2f);


        
               // currentSO = so[so.uniqueId];
                
    }

    void Update()
    {    
        // Si non modifiable, ne rien faire
        if (!isEditing) return;

        if (currentSO != null)
        {
            Debug.Log($"🎯 Objet sélectionné: {currentSO.typeName}");
        }

        if (currentSO != null && Input.GetMouseButtonDown(0))
{
 
if (currentSO.typeName == "EndWall")
{
    foreach (Transform t in sceneRoot.transform)
        if (t.name.Contains("EndWall")) { Destroy(t.gameObject); break; }

    placed.RemoveAll(p => p.type == "EndWall");
     //go.transform.rotation = Quaternion.Euler(0, 0, 90);
}

    Vector3 mousePos = Input.mousePosition;
    float distance = Mathf.Abs(Camera.main.transform.position.z);
    Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, distance));
    pos.z = 0;
   

    var go = Instantiate(currentSO.prefab, pos, Quaternion.identity, sceneRoot.transform);

    placed.Add(new Placeable {
        type = currentSO.typeName,
        x    = pos.x,
        y    = pos.y,
        rot  = go.transform.eulerAngles.z,
        sx   = go.transform.localScale.x,
        sy   = go.transform.localScale.y
    });
}
    }
 public void ExportJsonOnly()
{
    // On vérifie qu'il y a bien des objets placés avant d'exporter
    if (placed.Count == 0)
    {
        Debug.LogWarning("Aucun objet placé. Le JSON ne peut pas être créé.");
        return;
    }
    
    var data = new LevelData { objects = placed };
    string json = JsonUtility.ToJson(data, true);
    
    string directory = Application.dataPath + "/8 - Data";
    string baseFileName = "level_";
    
    while (File.Exists($"{directory}/{baseFileName}{currentFileIndex}.json"))
    {
        currentFileIndex++;
    }
    
    string path = $"{directory}/{baseFileName}{currentFileIndex}.json";
    if (currentSO.typeName=="Player" && placed.Exists(p=>p.type=="Player"))
{
    Debug.Log("Player déjà placé"); return;
}

    Directory.CreateDirectory(directory);
    File.WriteAllText(path, json);
    Debug.Log($"✅ JSON exporté → {path}");
    
    // Une fois sauvegardé, le placement est désactivé pour éviter la modification de la version sauvegardée.
    isEditing = false;
    
    // Vous pouvez aussi vider placed si vous ne souhaitez plus accumuler,
    //placed.Clear();
}

public void TestLevel()
{
    // Ici, on ne fait PAS d'export JSON, afin d'éviter de créer un fichier vide.
    // Si aucun objet n'a été placé, on affiche un message d'avertissement.
    if (placed.Count == 0)
    {
        Debug.LogWarning("Impossible de tester le niveau : aucun objet n'a été placé.");
        return;
    }
    
    // Désactive l'édition pour éviter d'ajouter d'autres objets lors du test
    isEditing = false;
    
    if (GameJson.Instance == null)
    {
        Debug.LogError("GameJson instance not found! Créez un GameObject avec le composant GameJson.");
        return;
    }
    
    // On utilise currentFileIndex qui correspond au dernier niveau sauvegardé (ou à sauvegarder)
    GameJson.Instance.SetLevelToLoad(currentFileIndex);
    UnityEngine.SceneManagement.SceneManager.LoadScene(8); 
}
}

// Classe pour décrire un objet placé (pour la sérialisation JSON)
[System.Serializable]
public class Placeable
{
    public string type;
    public float x;
    public float y;
    public float rot;
    public float sx;
    public float sy;
}


// Classe conteneur pour sérialiser la liste d'objets (racine du JSON)
[System.Serializable]
public class LevelData
{
    public List<Placeable> objects;
}
