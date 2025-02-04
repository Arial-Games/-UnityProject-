using System.Globalization;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using System.Collections.Generic;
using MyGameNamespace;



public class LevelLoader : MonoBehaviour
{
    public TextAsset level;
    public GameObject playerPrefab;
    public GameObject spikePrefab;
    public GameObject platformPrefab;
    public GameObject bonusPrefab;
    public Sprite backgroundSprite; 
    public Dictionary<string, GameObject> bonusPrefabs;
    void Start()
    {
    
    
         bonusPrefabs = new Dictionary<string, GameObject>
        {
            {"bonusType1", bonusPrefab1},
            {"bonusType2", bonusPrefab2},
        };
        XmlSerializer serializer = new XmlSerializer(typeof(Level));
        using (StringReader reader = new StringReader(level.text))
        {
            Level level = (Level)serializer.Deserialize(reader);
            InstantiateLevel(level);
        }
    }

    void ConfigureCameraFollow(GameObject player)
    {
        Camera mainCamera = Camera.main;

        if (mainCamera != null && player != null)
        {
            // Ajouter un script de suivi à la caméra
            CameraFollow cameraFollow = mainCamera.gameObject.AddComponent<CameraFollow>();
            cameraFollow.player = player.transform;

            // Configurez l'offset pour la caméra
            cameraFollow.offset = new Vector3(0, 0, -10); // Ajustez selon vos besoins
        }
        else
        {
            Debug.LogError("Impossible de configurer la caméra : caméra principale ou joueur introuvable.");
        }
    }
void ResizeBackground(SpriteRenderer renderer)
{
    Camera mainCamera = Camera.main;

    if (mainCamera != null && renderer != null)
    {
        // Obtenir les dimensions de la caméra en unités mondiales
        float screenHeight = mainCamera.orthographicSize * 2.0f;
        float screenWidth = screenHeight * mainCamera.aspect;

        // Obtenir les dimensions du sprite
        float spriteWidth = renderer.sprite.bounds.size.x;
        float spriteHeight = renderer.sprite.bounds.size.y;

        // Calculer le facteur d'échelle
        float scaleX = screenWidth / spriteWidth;
        float scaleY = screenHeight / spriteHeight;

        // Appliquer l'échelle au background
        renderer.transform.localScale = new Vector3(scaleX, scaleY, 1);
    }
    else
    {
        Debug.LogError("Impossible de redimensionner le background : caméra ou spriteRenderer introuvable.");
    }
}

    void InstantiateBackground(Level level)
{
    if (backgroundSprite != null)
    {
        GameObject backgroundObject = new GameObject("Background");
        SpriteRenderer renderer = backgroundObject.AddComponent<SpriteRenderer>();
        renderer.sprite = backgroundSprite;
        ResizeBackground(renderer);
        // Positionner le background
         backgroundObject.transform.position = new Vector3(0, 0, 1); // Z à 1 pour être plus proche

        renderer.sortingOrder = -10; // Ordre de tri pour le placer derrière

        // Faire du background un enfant de la caméra
        backgroundObject.transform.SetParent(Camera.main.transform);
    }
    else
    {
        Debug.LogError("Aucun sprite de background assigné dans l'inspecteur !");
    }
}


    void InstantiateLevel(Level level)
    {

        InstantiateBackground(level);

        // Instancier le joueur
        Vector2 playerPosition = ParsePosition(level.Player.StartPosition);
        GameObject player = Instantiate(playerPrefab, playerPosition, Quaternion.identity);

        // Configurer la caméra pour suivre le joueur
        ConfigureCameraFollow(player);

       foreach (var zone in level.ObstacleZones)
    {
    Vector2 minPos = ParsePosition(zone.MinPosition);
    Vector2 maxPos = ParsePosition(zone.MaxPosition);
    

    Instantiate(spikePrefab, minPos, Quaternion.identity);
    

    if (minPos != maxPos)
    {
        Instantiate(spikePrefab, maxPos, Quaternion.identity);
    }
    
    }

     foreach (var platform in level.Platforms)
        {
            Vector2 startPosition = ParsePosition(platform.Position);
            for (int i = 0; i < platform.Count; i++)
            {
                Vector2 position = startPosition + new Vector2(i * 1.0f, 0);
                Instantiate(platformPrefab, position, Quaternion.identity);
            }
      }
        
      foreach (var bonus in level.Bonuses)
        {
            Vector2 position = ParsePosition(bonus.Position);
            if (bonusPrefabs.ContainsKey(bonus.Type))
            {
                GameObject prefab = bonusPrefabs[bonus.Type];
                Instantiate(prefab, position, Quaternion.identity);
            }
            else
            {
                Debug.LogError("Préfabriqué non trouvé pour le type de bonus : " + bonus.Type);
            }
        }
    }

    Vector2 ParsePosition(string position)
    {
        string[] coordinates = position.Split(',');
        float x = float.Parse(coordinates[0], CultureInfo.InvariantCulture);
        float y = float.Parse(coordinates[1], CultureInfo.InvariantCulture);
        return new Vector2(x, y);
    }
}

namespace MyGameNamespace
{
    [XmlRoot("Level")]
    public class Level
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("difficulty")]
        public string Difficulty { get; set; }

        public Background Background { get; set; }
        public Player Player { get; set; }
        public List<Zone> ObstacleZones { get; set; }
        public List<Platform> Platforms { get; set; }
        public List<Bonus> Bonuses { get; set; }

}
    public class Background
    {
        [XmlAttribute("image")]
        public string Image { get; set; }
    }

    public class Player
    {
        [XmlAttribute("startPosition")]
        public string StartPosition { get; set; }

        [XmlAttribute("sprite")]
        public string Sprite { get; set; }
    }

    public class Zone
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("minPosition")]
        public string MinPosition { get; set; }

        [XmlAttribute("maxPosition")]
        public string MaxPosition { get; set; }

        [XmlAttribute("count")]
        public int Count { get; set; }
    }
    
 



    public class Platform
    {
        [XmlAttribute("sprite")]
        public string Sprite { get; set; }

        [XmlAttribute("position")]
        public string Position { get; set; }

        [XmlAttribute("count")]
        public int Count { get; set; }
    }

    public class Bonus
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("position")]
        public string Position { get; set; }
    }
}

