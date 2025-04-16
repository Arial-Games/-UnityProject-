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


    //-------------------
    //  METHODES DEFAULT
    //-------------------

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetRotation = Sprite.rotation;
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


    //-------------------
    //  METHODES PUBLIC
    //-------------------

    public void ToggleMode(int modeId)
    {
        if (modeId == 1)
        {
            CurrentMode = Mode.Normal;
            rb.gravityScale = 12.41067f;
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



    public bool IsOnCeiling()
    {
        return isOnCeiling;
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
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * 25, ForceMode2D.Impulse);
        RotateSprite(-90);
    }

    public void FlipGravity() // Wheals
    {
        rb.gravityScale *= -1;
        isOnCeiling = !isOnCeiling;
        RotateSprite(180);
    }

    // Triangle
    public void ToggleTriangleDirection()
    {
        isMovingUp = !isMovingUp;
        rb.velocity = new Vector2(12f, isMovingUp ? 12f : -12f);
        targetRotation = Quaternion.Euler(0, 0, isMovingUp ? 45 : -45);
    }

    void RotateSprite(float angle)
    {
        targetRotation *= Quaternion.Euler(0, 0, angle);
    }
}