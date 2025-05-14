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
              // ← fait disparaître le power-up au toucher
        }
    }

}



    
    private void Start()
    {
    }

public void ActivatePowerUp(string effectType, float powerUpDuration)
    {
        Debug.Log($"Démarrage du power-up {effectType} pour {powerUpDuration} secondes");
        Coroutine powerUpCoroutine = null;

        switch (effectType)
        {
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
            // ...existing cases...
        }

        if (powerUpCoroutine != null)
        {
            activeCoroutines.Add(powerUpCoroutine);
            Debug.Log($"Coroutine ajoutée pour {effectType}");
        }
    }

private IEnumerator SpeedBoost(float duration)
{
    if (playerController != null)
    {
            float d = Mathf.Min(duration, 2f);
        Debug.Log($"Activation speed boost pour {duration} secondes");
        originalSpeedIndex = (int)playerController.CurrentSpeed;
        int newSpeedIndex = Mathf.Min(originalSpeedIndex + 2, (int)Speed.SuperFast);
        playerController.CurrentSpeed = (Speed)newSpeedIndex;
        
        yield return new WaitForSeconds(d);
        
        Debug.Log("Désactivation speed boost");
        playerController.CurrentSpeed = (Speed)originalSpeedIndex;
        Destroy(gameObject); // Détruire le power-up après utilisation
    }
}

private IEnumerator MonsterMode(float duration)
{
    if (playerController != null)
    {
        Debug.Log($"Activation monster mode pour {duration} secondes");
        playerController.SetMonsterMode(true);
        
        yield return new WaitForSeconds(duration);
        
        Debug.Log("Désactivation monster mode");
        playerController.SetMonsterMode(false);
        Destroy(gameObject);
    }
}

private IEnumerator GhostMode(float duration)
{
    if (playerController != null)
    {
        Debug.Log($"Activation ghost mode pour {duration} secondes");
        playerController.SetGhostMode(true);
        
        yield return new WaitForSeconds(duration);
        
        Debug.Log("Désactivation ghost mode");
        playerController.SetGhostMode(false);
        Destroy(gameObject);
    }
}

private IEnumerator Invincibility(float duration)
{
    if (playerController != null)
    {
        Debug.Log($"Activation invincibility pour {duration} secondes");
        playerController.SetInvincible(true);
        
        yield return new WaitForSeconds(duration);
        
        Debug.Log("Désactivation invincibility");
        playerController.SetInvincible(false);
        Destroy(gameObject);
    }
}
   
/*private IEnumerator JumpBoost(float duration)
{
    if (playerController != null)
    {
        Debug.Log("Applying jump boost");
        playerController.SetJumpMultiplier(1.5f);
        
        yield return new WaitForSeconds(duration);
        
        Debug.Log("Restoring normal jump");
        playerController.SetJumpMultiplier(1f);
    }
}*/

    public bool IsInvincible()
    {
        return isInvincible;
    }
}