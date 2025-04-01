using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Object/GameHazards", order = 1)]
public class SO_GameHazards : ScriptableObject
{
    public enum Type { Jumper, Gate, Trap, Collectibles, Other, Nothing }
    public enum ObjVersion { Basic, Edited, Model}
    public enum TransformTo { Nothing, Basic, Triangle, Wheels, Navet, SpaceShip, Robot }
    public enum PowerOrbEffects {  Nothing, Speed, Jump, Invincibility, Monster, Explod }

    [Header("Common data")]
    public string publicName;
    [TextArea] public string description;
    public Type type;
    public ObjVersion objVersion;
    [Range(0, 1000)] public int uniqueId; // 1 -> 99 = Jumper // 100 -> 199 = Gate ...

    [Header("Jumper")]
    public bool isJumper;
    public int jumpStrenght;

    [Header("Gate")]
    public bool isGate;
    public TransformTo transformTo;
    public Sprite playerSkin;

    [Header("Trap")]
    public bool isTrap;
    public bool canKill;
    public bool canRotate;
    public int rotationSpeed;

    [Header("Collectibles")]
    public bool isCollectibles;
    public bool isStars;
    public int values;

    [Header("PowerUps")]
    public bool isPowerUp;
    public PowerOrbEffects powerOrbEffects;

    [Header("Other")]
    public bool isEndLine;
}
