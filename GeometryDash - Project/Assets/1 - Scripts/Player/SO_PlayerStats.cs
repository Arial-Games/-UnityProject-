using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Scriptable Object/Player Stats")]
public class SO_PlayerStat : ScriptableObject
{
    [HideInInspector] public UnityEvent update;

    [Header("Main data")] public string pseudo = "NewPlayer";
    public int actualSkinId = 0;

    [Header("Ressources")]
    public float gold = 0, cash = 0, stars = 0, starsCoins = 0;

    [Header("Other")]
    public int XP = 0, playerLevel = 0, storyProgression = 0;
    public bool openSave = false;
    public bool[] possesionId;


    private void OnEnable()
    {
        // Quand l'instance est activée

        if (update == null)
            update = new UnityEvent();
    }

    private void OnValidate()
    {
        // Quand l'instance recoit une modif
        // Dans l'inspector

        update.Invoke();
    }
}
