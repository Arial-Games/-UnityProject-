using UnityEngine;

public class GameJson : MonoBehaviour
{
    public static GameJson Instance { get; private set; }
    public int currentLevelToLoad { get; private set; }  

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetLevelToLoad(int level)
    {
        currentLevelToLoad = level;
        Debug.Log($"Level to load set to: {level}");
    }
    public int GetLevelToLoad()
    {
        return currentLevelToLoad;
    }
}