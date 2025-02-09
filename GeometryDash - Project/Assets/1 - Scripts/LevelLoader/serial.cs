using System.Globalization;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using System.Collections.Generic;
using MyGameNamespace;



public class LevelLoader : MonoBehaviour
{

    public Sprite gateBackgroundSprite;
    public TextAsset level;
    public GameObject playerPrefab;
    public GameObject spikePrefab;
    public GameObject platformPrefab;
    public GameObject bonusPrefab1;
    public GameObject bonusPrefab2;
    public GameObject bonusPrefab3;
    public GameObject bonusPrefab4;
    public GameObject bonusPrefab5;
    //public Sprite backgroundSprite; 
    public GameObject barrierPrefab1;
    public GameObject barrierPrefab2;
    public GameObject barrierPrefab3;
    public GameObject barrierPrefab4;
    public GameObject barrierPrefab5;
    public GameObject barrierPrefab6;
    public Dictionary<string, GameObject> barrierPrefabs;

    public Dictionary<string, GameObject> bonusPrefabs;
    void Start()
    {


        bonusPrefabs = new Dictionary<string, GameObject>
        {
            {"bonusType1", bonusPrefab1},
            {"bonusType2", bonusPrefab2},
            {"bonusType3", bonusPrefab3},
            {"bonusType4", bonusPrefab4},
            {"bonusType5", bonusPrefab5},

        };
        barrierPrefabs = new Dictionary<string, GameObject>
          {
           { "barrierType1", barrierPrefab1 },
           { "barrierType2", barrierPrefab2 },
           { "barrierType3", barrierPrefab3 },
           { "barrierType4", barrierPrefab4 },
           { "barrierType5", barrierPrefab5 },
           { "barrierType6", barrierPrefab6 },
         };

        XmlSerializer serializer = new XmlSerializer(typeof(Level));
        using (StringReader reader = new StringReader(level.text))
        {
            Level level = (Level)serializer.Deserialize(reader);
            InstantiateLevel(level);
        }
    }

    public int repeatCount = 7;
    void InstantiateMiddleBackground(Level level)
    {
        if (level.MiddleBackground != null && !string.IsNullOrEmpty(level.MiddleBackground.Image))
        {
            Sprite middleSprite = gateBackgroundSprite;
            if (middleSprite != null)
            {
                Vector3 startPos = Vector3.zero;
                if (!string.IsNullOrEmpty(level.MiddleBackground.Position))
                {
                    string[] coords = level.MiddleBackground.Position.Split(',');
                    float x = float.Parse(coords[0], CultureInfo.InvariantCulture);
                    float y = float.Parse(coords[1], CultureInfo.InvariantCulture);
                    float z = (coords.Length > 2) ? float.Parse(coords[2], CultureInfo.InvariantCulture) : 0f;
                    startPos = new Vector3(x, y, z);
                }
                float spriteWidth = middleSprite.bounds.size.x;
                for (int i = 0; i < repeatCount; i++)
                {
                    GameObject mb = new GameObject("MiddleBackground_" + i);
                    SpriteRenderer renderer = mb.AddComponent<SpriteRenderer>();
                    renderer.sprite = middleSprite;
                    mb.transform.position = startPos + new Vector3(i * spriteWidth, 0, 0);
                    renderer.sortingOrder = -5;
                }
            }
            else
            {
                Debug.LogError("Sprite MiddleBackground non trouvé : " + level.MiddleBackground.Image);
            }
        }
    }

    /*    void ConfigureCameraFollow(GameObject player)
        {
            Camera mainCamera = Camera.main;

            if (mainCamera != null && player != null)
            {

                CameraFollow cameraFollow = mainCamera.gameObject.AddComponent<CameraFollow>();
                cameraFollow.player = player.transform;


                cameraFollow.offset = new Vector3(0, 0, -10); 
            }
            else
            {
                Debug.LogError("Impossible de configurer la caméra : caméra principale ou joueur introuvable.");
            }
        }*/
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

    /*    void InstantiateBackground(Level level)
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
    }*/


    void InstantiateLevel(Level level)
    {

        //InstantiateBackground(level);
        InstantiateMiddleBackground(level);
        // Instancier le joueur
        Vector2 playerPosition = ParsePosition(level.Player.StartPosition);
        GameObject player = Instantiate(playerPrefab, playerPosition, Quaternion.identity);

        // Configurer la caméra pour suivre le joueur
        //ConfigureCameraFollow(player);

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

        foreach (var barrier in level.Barriers)
        {
            Vector2 startPos = ParsePosition(barrier.Position);
            if (barrierPrefabs.ContainsKey(barrier.Type))
            {
                GameObject prefab = barrierPrefabs[barrier.Type];
                for (int i = 0; i < barrier.Count; i++)
                {
                    Vector2 pos = startPos + new Vector2(i * 1.0f, 0);
                    GameObject instance = Instantiate(prefab, pos, Quaternion.identity);
                    if (barrier.Type == "barrierType6")
                    {
                        instance.transform.rotation = Quaternion.Euler(0, 0, 90);
                    }
                }
            }
            else
            {
                Debug.LogError("Préfabriqué pour barrier non trouvé : " + barrier.Type);
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
        public Background MiddleBackground { get; set; }
        public Background Background { get; set; }
        public Player Player { get; set; }
        public List<Zone> ObstacleZones { get; set; }
        public List<Platform> Platforms { get; set; }
        public List<Bonus> Bonuses { get; set; }

        public List<Barrier> Barriers { get; set; }
    }
    public class Background
    {
        [XmlAttribute("image")]
        public string Image { get; set; }
        [XmlAttribute("position")]
        public string Position { get; set; }
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


    public class MiddleBackground
    {
        [XmlAttribute("image")]
        public string Image { get; set; }

        [XmlAttribute("position")]
        public string Position { get; set; }  // nouvel attribut
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


    public class Barrier
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

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

