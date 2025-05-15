using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PowerUpBehaviour : MonoBehaviour
{
    private PlayerController playerController;
    private bool isInvincible = false;
    private int originalSpeedIndex;
    public float duration;  // ← pour accueillir powerUp.Duration
    public string effect;

    private List<Coroutine> activeCoroutines = new List<Coroutine>();

    private void OnDisable()
    {
        // Stop all active power-up coroutines when disabled
        foreach (var coroutine in activeCoroutines)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
            }
        }
        activeCoroutines.Clear();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                ActivatePowerUp(effect, duration);

            }
        }

    }


    private void Start()
    {
    }

    public void ActivatePowerUp(string effectType, float powerUpDuration)
    {
        Coroutine powerUpCoroutine = null;

        switch (effectType)
        {
            case "explode":
                if (playerController != null)
                {
                    playerController.Explode();
                    Destroy(gameObject); // Détruire le power-up après utilisation
                }
                break;
            case "speed_2x":
                powerUpCoroutine = StartCoroutine(SpeedBoost(powerUpDuration));
                break;
            case "invincible":
                powerUpCoroutine = StartCoroutine(Invincibility(powerUpDuration));
                break;
            case "ghost":
                powerUpCoroutine = StartCoroutine(GhostMode(powerUpDuration));
                break;
            case "monster":
                powerUpCoroutine = StartCoroutine(MonsterMode(powerUpDuration));
                break;
            default:
                Debug.LogWarning($"Type de power-up non reconnu: {effectType}");
                break;
        }

        if (powerUpCoroutine != null)
        {
            activeCoroutines.Add(powerUpCoroutine);
        }
    }

    private IEnumerator SpeedBoost(float duration)
    {
        if (playerController != null)
        {
            float d = Mathf.Min(duration, 2f);
            originalSpeedIndex = (int)playerController.CurrentSpeed;
            int newSpeedIndex = Mathf.Min(originalSpeedIndex + 2, (int)Speed.SuperFast);
            playerController.CurrentSpeed = (Speed)newSpeedIndex;

            yield return new WaitForSeconds(d);

            playerController.CurrentSpeed = (Speed)originalSpeedIndex;
            //Destroy(gameObject);
        }
    }

    private IEnumerator MonsterMode(float duration)
    {
        if (playerController != null)
        {
            playerController.SetMonsterMode(true);

            yield return new WaitForSeconds(duration);

            playerController.SetMonsterMode(false);
            Destroy(gameObject);
        }
    }

    private IEnumerator GhostMode(float duration)
    {
        if (playerController != null)
        {
            playerController.SetGhostMode(true);

            yield return new WaitForSeconds(duration);

            playerController.SetGhostMode(false);
            //Destroy(gameObject);
        }
    }

    private IEnumerator Invincibility(float duration)
    {
        if (playerController != null)
        {
            playerController.SetInvincible(true);

            yield return new WaitForSeconds(duration);

            playerController.SetInvincible(false);
            //Destroy(gameObject);
        }
    }

    /*private IEnumerator JumpBoost(float duration)
    {
        if (playerController != null)
        {
            playerController.SetJumpMultiplier(1.5f);

            yield return new WaitForSeconds(duration);

            playerController.SetJumpMultiplier(1f);
        }
    }*/

    public bool IsInvincible()
    {
        return isInvincible;
    }
}