using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Speed
{
    Slow = 0,
    Normal = 1,
    Fast = 2,
    VeryFast = 3,
    SuperFast = 4,
}

public enum Mode
{
    Normal,
    Ship,
    Wheels,
    Triangle
}

public class PlayerController : MonoBehaviour
{
    public Speed CurrentSpeed;
    public Mode CurrentMode = Mode.Normal;

    float[] speedValues = { 8.6f, 10.4f, 12.96f, 15.6f, 19.27f };

    public Transform GroundCheckTransform;
    public float GroundCheckRadius;
    public LayerMask GroundMask;

    public Transform Sprite;
    private Rigidbody2D rb;
    private bool isGrounded;
    private Quaternion targetRotation;
    private float rotationSpeed = 500f;
    private bool isOnCeiling = false;
    private bool isMovingUp = true;
    private bool isGhost = false;
    private bool isInvincible = false;
    private bool isMonster = false;
    private Vector3 originalScale;
    [Header("Audio"), SerializeField] AudioSource audioJump;
    public float explosionRadius = 5f;
    public LayerMask explosionMask;


    //-------------------
    //  METHODES DEFAULT
    //-------------------

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetRotation = Sprite.rotation;
        originalScale = transform.localScale;

    }

    void Update()
    {
        if (Time.timeScale == 0f) return;

        transform.position += Vector3.right * speedValues[(int)CurrentSpeed] * Time.deltaTime;
        isGrounded = OnGround();

        if (CurrentMode == Mode.Normal)
        {
            if (isGrounded && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
            {
                Jump();
            }
        }
        else if (CurrentMode == Mode.Ship)
        {
            if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, 12f);
            }
            targetRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (CurrentMode == Mode.Triangle)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                ToggleTriangleDirection();
            }
        }
        else if (CurrentMode == Mode.Wheels)
        {
            if (isGrounded || isOnCeiling)
            {
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                {
                    FlipGravity();
                }
            }
        }

        Sprite.rotation = Quaternion.RotateTowards(Sprite.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void Die()
    {
        Debug.Log("Player died");

        // Reset all power-up effects
        ResetAllPowerUps();

        // Find GameRules and call OnPlayerDeath
        var gameRules = FindObjectOfType<GameRules>();
        if (gameRules != null)
        {
            gameRules.OnPlayerDeath();
        }
    }

    private void ResetAllPowerUps()
    {
        // Reset all power-up states
        isGhost = false;
        isInvincible = false;
        isMonster = false;

        // Reset visual effects
        var sprite = Sprite.GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = Color.white;
        }

        // Reset scale if in monster mode
        transform.localScale = originalScale;

        // Reset layer to default player layer
        gameObject.layer = LayerMask.NameToLayer("Player");

        Debug.Log("All power-up effects reset on death");
    }
    public void SetMonsterMode(bool enabled)
    {
        isMonster = enabled;
        // Utiliser le Transform Sprite qui est déjà référencé
        var sprite = Sprite.GetComponent<SpriteRenderer>();

        if (enabled)
        {
            Debug.Log("Activation mode monstre - échelle: " + originalScale);
            // Grandir et devenir rouge
            transform.localScale = originalScale * 1.5f;
            if (sprite != null)
            {
                sprite.color = new Color(1f, 0.2f, 0.2f, 1f);
            }
        }
        else
        {
            Debug.Log("Désactivation mode monstre - retour à l'échelle: " + originalScale);
            // Retour à la normale
            transform.localScale = originalScale;
            if (sprite != null)
            {
                sprite.color = Color.white;
            }
        }

        Debug.Log($"Monster mode: {(enabled ? "activated" : "deactivated")}");
    }
    private bool showExplosionRadius = false;
    private float explosionTime = 0f;

    public void Explode()
    {
        Debug.Log("Explosion activated!");

        // Effet visuel
        var sprite = Sprite.GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            StartCoroutine(FlashEffect(sprite));
        }

        // Augmenter le rayon et la détection
        float radius = explosionRadius * 2f; // Double the radius for better detection
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, LayerMask.GetMask("Default"));
        Debug.Log($"Found {colliders.Length} colliders in explosion radius");

        foreach (Collider2D col in colliders)
        {
            Debug.Log($"Checking object: {col.gameObject.name}");

            // Check for barriers and spikes with more flexible naming
            if (col.gameObject.name.ToLower().Contains("barrier") ||
                col.gameObject.name.ToLower().Contains("spike") ||
                col.gameObject.name.ToLower().Contains("Bonus"))
            {
                Debug.Log($"Destroying: {col.gameObject.name}");
                Destroy(col.gameObject);
            }
        }

        // Visual feedback
        showExplosionRadius = true;
        explosionTime = Time.time;

        // Add force to nearby objects (optional)
        foreach (Collider2D col in colliders)
        {
            Rigidbody2D rb = col.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (col.transform.position - transform.position).normalized;
                rb.AddForce(direction * 10f, ForceMode2D.Impulse);
            }
        }
    }
    private void OnDrawGizmos()
    {
        // Afficher le cercle d'explosion pendant 1 seconde
        if (showExplosionRadius && Time.time - explosionTime < 1f)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRadius);
        }
        else
        {
            showExplosionRadius = false;
        }
    }

    private IEnumerator FlashEffect(SpriteRenderer sprite)
    {
        Color originalColor = sprite.color;
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = originalColor;
    }
    //-------------------
    //  METHODES PUBLIC
    //-------------------

    public void ToggleMode(int modeId)
    {
        if (modeId == 1)
        {
            CurrentMode = Mode.Normal;
            rb.gravityScale = 12.41067f;
            targetRotation = Quaternion.Euler(0, 0, 0);
        }
        else if (modeId == 2)
        {
            CurrentMode = Mode.Ship;
            rb.gravityScale = 4f;
        }
        else if (modeId == 3)
        {
            CurrentMode = Mode.Wheels;
            rb.gravityScale = 12.41067f;
        }
        else if (modeId == 4)
        {
            CurrentMode = Mode.Triangle;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(12f, isMovingUp ? 12f : -12f);
            targetRotation = Quaternion.Euler(0, 0, isMovingUp ? 45 : -45);
        }
    }
    public void SetInvincible(bool enabled)
    {
        isInvincible = enabled;
        // Effet visuel doré
        var sprite = GetComponentInChildren<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.color = enabled ? new Color(1f, 0.8f, 0f, 1f) : Color.white;
        }
        Debug.Log($"Invincible mode: {(enabled ? "activated" : "deactivated")}");
    }

    public bool IsOnCeiling()
    {
        return isOnCeiling;
    }
  public void SetGhostMode(bool enabled)
{
    isGhost = enabled;
    
    // Use a constant for the layer numbers
    const int GHOST_LAYER = 8;  // Use whatever number you assigned in Unity
    const int PLAYER_LAYER = 0; // Default layer
    
    // Set the layer
    gameObject.layer = enabled ? GHOST_LAYER : PLAYER_LAYER;
    
    // Visual effect
    var sprite = GetComponentInChildren<SpriteRenderer>();
    if (sprite != null)
    {
        sprite.color = enabled ? new Color(0.5f, 0.5f, 1f, 0.5f) : Color.white;
    }
    
    Debug.Log($"Ghost mode {(enabled ? "enabled" : "disabled")} on layer {gameObject.layer}");
}
    //-------------------
    //  METHODES PRIVEE
    //-------------------

    public bool OnGround()
    {
        return Physics2D.OverlapCircle(GroundCheckTransform.position, GroundCheckRadius, GroundMask);
    }

    public void Jump()
    {
        audioJump.Play();
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * 25, ForceMode2D.Impulse);
        RotateSprite(-90);
    }

    public void FlipGravity() // Wheals
    {
        audioJump.Play();
        rb.gravityScale *= -1;
        isOnCeiling = !isOnCeiling;
        RotateSprite(180);
    }

    // Triangle
    public void ToggleTriangleDirection()
    {
        audioJump.Play();
        isMovingUp = !isMovingUp;
        rb.velocity = new Vector2(12f, isMovingUp ? 12f : -12f);
        targetRotation = Quaternion.Euler(0, 0, isMovingUp ? 45 : -45);
    }

    void RotateSprite(float angle)
    {
        targetRotation *= Quaternion.Euler(0, 0, angle);
    }
}