using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class PlayersScore : MonoBehaviour
{
    [SerializeField] LevelSaveData levelSaveData;
    [SerializeField] SaveData saveData;
    [SerializeField] GameObject[] playerScoreGO;
    [SerializeField] TextMeshProUGUI[] playerScoreText;
    [SerializeField] SO_PlayersSkins[] so_skins;

    private List<(string name, int score)> players = new List<(string, int)>();

    void Start()
    {
        UpdateScores();
    }

    public void UpdateScores()
    {
        players.Clear();

        var playerData = saveData.GetPlayerData();

        int bestScore = levelSaveData.LoadGlobalScore();
        players.Add((playerData.pseudo, bestScore));

        int[] fixedScores = { 12078, 25019, 33778, 45846, 50012 };
        int nbPlayers = Mathf.Min(playerScoreGO.Length, fixedScores.Length + 1);

        for (int i = 1; i < nbPlayers; i++)
        {
            if (i < so_skins.Length)
                players.Add((so_skins[i].publicName, fixedScores[i - 1]));
            else
                players.Add(("Unknown", fixedScores[i - 1]));
        }

        players = players.OrderByDescending(p => p.score).ToList();

        for (int i = 0; i < nbPlayers; i++)
        {
            playerScoreGO[i].GetComponentInChildren<TextMeshProUGUI>().text = players[i].name;
            playerScoreGO[i].GetComponentInChildren<TextMeshProUGUI>().color = so_skins[i % so_skins.Length].mainColor;
            playerScoreText[i].text = "Score : " + players[i].score;

            Image imageComponent = playerScoreGO[i].transform.Find("PlayerPP")?.GetComponent<Image>();
            if (imageComponent != null)
            {
                if (players[i].name == playerData.pseudo)
                {
                    imageComponent.sprite = so_skins[playerData.skinId].skinSprite;
                }
                else
                {
                    imageComponent.sprite = so_skins[i % so_skins.Length].skinSprite;
                }
            }
        }
    }
}
