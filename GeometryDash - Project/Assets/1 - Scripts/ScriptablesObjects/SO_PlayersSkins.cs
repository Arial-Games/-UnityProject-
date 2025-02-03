using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Object/PlayerSkins", order = 1)]
public class SO_PlayersSkins : ScriptableObject
{
    public enum Type {  Basic, Triangle, Navette, Robot, Wheels, Other }
    public enum Rarety { Commun, Atypique, Rare, Legendary, Mytique, Collection };
    public enum MoneyNeed { Cash, Gold, Stars, StarsCoins};

    [Header("Basic info")]
    public string publicName;
    [TextArea] public string description;
    public Type type;
    [Range(0, 2000)] public int price;
    public MoneyNeed moneyNeed;

    [Header("Back info")]
    [Range(0, 6)] public int uniqueId;
    public Rarety rarety;

    [Header("Visuel")]
    public Sprite skinSprite;
    public Color mainColor;
    public Color secColor;
}
