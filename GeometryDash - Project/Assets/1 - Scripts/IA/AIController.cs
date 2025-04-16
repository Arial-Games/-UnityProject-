using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AIController : MonoBehaviour
{
    public PlayerController player;
    private AIData aiData;
    private List<bool> currentRun = new List<bool>();
    private float startX;
    private bool hasFailed;

    void Start()
    {
        aiData = AISaveManager.Load();
        startX = transform.position.x;
        hasFailed = false;
        if (player == null) player = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Time.timeScale == 0f || hasFailed) return;

        SimulateInput();

        if (IsDead())
        {
            hasFailed = true;
            RunAI();
        }
    }

    float pressCooldown = 0.3f;
    float lastPressTime = 0f;

    void SimulateInput()
    {
        if (Time.time - lastPressTime < pressCooldown)
        {
            currentRun.Add(false);
            return;
        }

        bool shouldPress = Random.value > 0.8f; // seulement 20% de chances de presser
        currentRun.Add(shouldPress);

        if (shouldPress)
        {
            lastPressTime = Time.time;

            if (player.CurrentMode == Mode.Normal && player.OnGround())
            {
                player.Jump();
            }
            else if (player.CurrentMode == Mode.Ship)
            {
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, 12f);
            }
            else if (player.CurrentMode == Mode.Triangle)
            {
                player.ToggleTriangleDirection();
            }
            else if (player.CurrentMode == Mode.Wheels && (player.OnGround() || player.IsOnCeiling()))
            {
                player.FlipGravity();
            }
        }
    }


    bool IsDead()
    {
        return transform.position.y < -10;
    }

    void RunAI()
    {
        float distance = transform.position.x - startX;
        Debug.Log("AI finished run. Distance: " + distance + ", Best: " + aiData.bestDistance);

        if (distance > aiData.bestDistance)
        {
            aiData.bestDistance = distance;
            aiData.bestRun = new List<bool>(currentRun);
            AISaveManager.Save(aiData);
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
