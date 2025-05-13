using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Object/WorkshopObjects", order = 1)]
public class SO_WorkshopObjects : ScriptableObject
{
    public enum Type { Player, LevelBuild, Trap, Gate, Bumper, Other }

    [Header("Basic info")]
    public Sprite objSprite;
    public string publicName;
    [TextArea] public string description;
    public Type type;
    [Range(0, 100)] public int dataSize; // Pour limiter la taille des niveaux

    [Header("Back info")]
    [Range(0, 20)] public int uniqueId;
}*/

[CreateAssetMenu(fileName = "WorkshopObject", menuName = "Workshop/Object", order = 1)]
public class SO_WorkshopObjects : ScriptableObject
{
    public int    uniqueId;
    public string typeName;      // ex. "Platform", "Spike", etc.
    public Sprite objSprite;     // icône et sprite visual
    public GameObject prefab;  
 [Range(0, 100)] public int dataSize;
 }
