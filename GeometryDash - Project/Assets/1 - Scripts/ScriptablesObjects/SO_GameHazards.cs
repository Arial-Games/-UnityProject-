using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Object/GameHazards", order = 1)]
public class SO_GameHazards : ScriptableObject
{
    public enum Type { Jumper, Gate, Trap, Collectibles}
    public enum TransformTo { Basic, Triangle, Wheels, Navet, SpaceShip, Robot }

    [Header("Common data")]
    public string publicName;
    [TextArea] public string description;
    public Type type;
    [Range(0, 20)] public int uniqueId;

    [Header("Jumper")]
    public bool isJumper;
    public bool jumpStrenght;

    [Header("Gate")]
    public bool isGate;
    public TransformTo transformTo;

    [Header("Trap")]
    public bool isTrap;
    public bool canKill;

    [Header("Collectibles")]
    public bool isCollectibles;
    public int values;
}
