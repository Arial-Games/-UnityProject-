using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Object/ShopObjects", order = 1)]
public class SO_ShopObjects : ScriptableObject
{
    public enum Type { Booster, Bonus, Level, Other }
    public enum Rarety { Commun, Atypique, Rare, Legendary, Mytique, Collection };

    [Header("Basic info")]
    public Sprite objSprite;
    public string publicName;
    [TextArea] public string description;
    public Type type;
    [Range(0, 100)] public int price;

    [Header("Back info")]
    [Range(0, 20)] public int uniqueId;
    public Rarety rarety;
}
