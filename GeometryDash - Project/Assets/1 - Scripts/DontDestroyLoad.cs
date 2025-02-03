using UnityEngine;

public class DontDestroyLoad : MonoBehaviour
{
    private static DontDestroyLoad instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
