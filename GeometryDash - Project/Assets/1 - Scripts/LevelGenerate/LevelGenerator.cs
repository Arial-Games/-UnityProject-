using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject playerPrefab;
    //public GameObject startPrefab;
    public GameObject endPrefab;
    public GameObject trapPrefab;
    public GameObject portalPrefab;
    public GameObject starPrefab;
    public GameObject floorPrefab;
    public GameObject ceilingPrefab;

    public float levelLengthMin = 50f;
    public float levelLengthMax = 600f;
    public int trapCountMin = 3;
    public int trapCountMax = 10;
    public float floorY = 0f;
    public float ceilingY = 4f;
    public float safeZone = 10f; // Zone sans hazards au début

    void Start()
    {
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        // Nettoyer l'ancien niveau
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        float levelLength = Random.Range(levelLengthMin, levelLengthMax);

        // Instancier le sol sur toute la longueur, à la hauteur floorY
        Vector3 floorPos = new Vector3(levelLength / 2, floorY, 0);
        GameObject floor = Instantiate(floorPrefab, floorPos, Quaternion.identity, transform);
        floor.transform.localScale = new Vector3(levelLength, 1, 1);

        // Instancier le plafond (à sa position d'origine)
        Vector3 ceilingPos = new Vector3(levelLength / 2, ceilingY, 0);
        GameObject ceiling = Instantiate(ceilingPrefab, ceilingPos, Quaternion.identity, transform);
        ceiling.transform.localScale = new Vector3(levelLength, 1, 1);

        // Départ et Fin sur le sol
        //Vector3 startPos = new Vector3(0, floorY, 0);
        //Instantiate(startPrefab, startPos, Quaternion.identity, transform);
        Vector3 endPos = new Vector3(levelLength, floorY+1.3f, 0);
        Instantiate(endPrefab, endPos, Quaternion.identity, transform);

        // Instancier le joueur dans la safe zone (ici, dans le début du niveau)
        float playerX = Random.Range(0f, safeZone);
        Vector3 playerPos = new Vector3(playerX, floorY + 0.1f, 0);
        Instantiate(playerPrefab, playerPos, Quaternion.identity, transform);

        // Placer les pièges sur le sol, dans la safe zone (hors début de niveau si besoin)
        int trapCount = Random.Range(trapCountMin, trapCountMax);
        float minSpaceBetweenTraps = 6f; // Espace minimum entre les pièges
        float lastTrapX = safeZone; // Position du dernier piège placé

        for (int i = 0; i < trapCount; i++)
        {
            float minX = lastTrapX + minSpaceBetweenTraps;
            float maxX = levelLength - 5f;
    
        if (minX < maxX)
        {
        float trapX = Random.Range(minX, maxX);
        Instantiate(trapPrefab, new Vector3(trapX, floorY + 1.3f, 0), Quaternion.identity, transform);
        lastTrapX = trapX;
        }
        }

        // Placer le portail sur le sol, hors zone safe
        float portalX = Random.Range(levelLength * 0.6f, levelLength - 10f);
        Instantiate(portalPrefab, new Vector3(portalX, floorY + 1.7f, 0), Quaternion.identity, transform);

        // Placer exactement 3 étoiles sur le sol, dans la safe zone
        for (int i = 0; i < 3; i++)
        {
            float starX = Random.Range(safeZone, levelLength - 5f);
            Instantiate(starPrefab, new Vector3(starX, floorY + 1.5f, 0), Quaternion.identity, transform);
        }
    }
}

