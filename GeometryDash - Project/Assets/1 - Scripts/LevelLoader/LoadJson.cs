using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public class LoadJson : MonoBehaviour
{
    [Header("Level Loading")]
    [SerializeField] private Transform levelRoot;
    //[SerializeField] private GameObject playerPrefab;
    [SerializeField] private SO_WorkshopObjects[] availableObjects;
    [SerializeField] private Vector3 playerSpawnPosition = new Vector3(-20, 0, 0);
    private Dictionary<string, GameObject> lookup;
        //srializeField] private GameObject playerPrefab;              // ← ajoute ça
    void Awake()
    {
        lookup = availableObjects
            .ToDictionary(so => so.typeName, so => so.prefab);

            
    }
  void Start()
    {
        
        // Avant de charger le JSON, place le player


        // Verify required references
        if (levelRoot == null || availableObjects == null)
        {
            Debug.LogError("❌ References manquantes dans l'Inspector!");
            return;
        }

        // Clean existing objects
        foreach (Transform child in levelRoot)
        {
            Destroy(child.gameObject);
        }

      // (playerPrefab != null)
          //Instantiate(playerPrefab, playerSpawnPosition, Quaternion.identity, levelRoot); 

        // Instantiate player at spawn position
        //GameObject player = Instantiate(playerPrefab, playerSpawnPosition, Quaternion.identity);
        //player.tag = "Player"; // Ensure player is tagged correctly

        // Load level
        if (GameJson.Instance != null)
        {
            int levelToLoad = GameJson.Instance.currentLevelToLoad;
            Debug.Log($"📥 Chargement du niveau {levelToLoad}");
            LoadJsonLevel(levelToLoad);
        }
        else
        {
            Debug.Log("📥 Chargement du dernier niveau créé");
            LoadJsonLevel(-1);
        }
    }
private string GetLatestJsonLevel()
{
    string dataPath = Path.Combine(Application.dataPath, "8 - Data");
    if (!Directory.Exists(dataPath))
    {
        Debug.LogError("❌ Dossier Data non trouvé!");
        return null;
    }

    var jsonFiles = Directory.GetFiles(dataPath, "level_*.json")
                            .OrderByDescending(f => new FileInfo(f).CreationTime)
                            .ToArray();

    if (jsonFiles.Length == 0)
    {
        Debug.LogError("❌ Aucun fichier level_*.json trouvé dans le dossier Data!");
        return null;
    }

    return jsonFiles[0];
}
public void LoadJsonLevel(int levelNum = -1)
{
    /* ---------- chemin ---------- */
    string path = levelNum <= 0
        ? GetLatestJsonLevel() ?? Path.Combine(Application.dataPath, "8 - Data", "level_1.json")
        : Path.Combine(Application.dataPath, "8 - Data", $"level_{levelNum}.json");
    if (!File.Exists(path)) { Debug.LogError("❌ Fichier introuvable"); return; }

    try
    {
        foreach (Transform c in levelRoot) Destroy(c.gameObject);

        /* ---------- parse JSON ---------- */
        var data = JsonUtility.FromJson<LevelData>(File.ReadAllText(path));
        var patternObjects = data.objects.ToList();
        if (patternObjects.Count == 0) return;

        /* ---------- extraire EndWall ---------- */
       var endWall = patternObjects.FirstOrDefault(o => o.type == "EndWall");
          patternObjects.RemoveAll(o => o.type == "EndWall");

        /* ---------- largeur du pattern (sans EndWall) ---------- */
        float minX = patternObjects.Min(o => o.x);
        float maxX = patternObjects.Max(o => o.x + o.sx * lookup[o.type].transform.localScale.x);
        float patternWidth = maxX - minX;

        /* ---------- listes ---------- */
        var blacklist = new[] { "Basic-SpaceShipGate 1", "Basic-CubeGate" };
        int repetitions = 5;

        /* ---------- pattern original ---------- */
        
        foreach (var obj in patternObjects)
        {
             if (!lookup.ContainsKey(obj.type)) continue; // saute “Basic-player”
    if (blacklist.Contains(obj.type)) continue;
    var prefab = lookup[obj.type];
    Instantiate(prefab, new Vector3(obj.x, obj.y, 0),
                Quaternion.Euler(0,0,obj.rot), levelRoot)
       .transform.localScale = new Vector3(obj.sx, obj.sy, 1);
        }

        /* ---------- répétitions ---------- */
     for (int r = 1; r < repetitions; r++)
{
    foreach (var obj in patternObjects)
    {
        if (blacklist.Contains(obj.type)) 
            continue;

        GameObject pf;
       
         if (!lookup.TryGetValue(obj.type, out pf))
        {
            continue;
        }

        Vector3 pos = new Vector3(obj.x + r * patternWidth, obj.y, 0);
        var inst = Instantiate(pf, pos, Quaternion.Euler(0, 0, obj.rot), levelRoot);
        inst.transform.localScale = new Vector3(obj.sx, obj.sy, 1);
    }
}

        /* ---------- objets blacklistés uniques ---------- */
        foreach (var obj in patternObjects.Where(o => blacklist.Contains(o.type)))
        {
            if (!lookup.TryGetValue(obj.type, out var prefab)) continue;
            var inst = Instantiate(prefab, new Vector3(obj.x, obj.y, 0),
                                   Quaternion.Euler(0, 0, obj.rot), levelRoot);
            inst.transform.localScale = new Vector3(obj.sx, obj.sy, 1);
        }

        /* ---------- EndWall après la dernière répétition ---------- */
 if (endWall != null && lookup.TryGetValue("EndWall", out var endPrefab))
{
    float finalX = minX + repetitions * patternWidth;
    var go = Instantiate(
        endPrefab,
        new Vector3(finalX, endWall.y, 0),
        Quaternion.Euler(0, 0, endWall.rot + 90f),   // ← vertical
        levelRoot);

    go.transform.localScale = new Vector3(endWall.sx, endWall.sy, 1);
}

if (lookup.TryGetValue("Roof", out var roofPrefab))
{
float firstRoofX = patternObjects
    .Where(o => o.type == "Roof")
    .Select(o => o.x)
    .DefaultIfEmpty(0)
    .Min();

for (float x = playerSpawnPosition.x + 2f; x < firstRoofX; x += 1f)
{
    Vector3 pos = new Vector3(x, playerSpawnPosition.y - 1f, 0);
    var r = Instantiate(roofPrefab, pos, Quaternion.identity, levelRoot);
    r.transform.localScale = Vector3.one;
}

}


    }
    catch (System.Exception e)
    {
        Debug.LogError($"❌ Erreur : {e.Message}");
    }
}
}