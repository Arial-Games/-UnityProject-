using System.IO;
using UnityEngine;

public static class AISaveManager
{
    private static string path => Path.Combine(Application.persistentDataPath, "ai_save.json");

    public static void Save(AIData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log("AI data saved to: " + path);
    }

    public static AIData Load()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<AIData>(json);
        }
        return new AIData();
    }
}
