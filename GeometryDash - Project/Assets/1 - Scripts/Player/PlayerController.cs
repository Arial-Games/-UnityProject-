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
    Ship
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
        transform.position += Vector3.right * speedValues[(int)CurrentSpeed] * Time.deltaTime;
        isGrounded = OnGround();

        //if (Input.GetKeyDown(KeyCode.U))
        //{
        //    ToggleMode();
        //}

        if (CurrentMode == Mode.Normal)
        {
            if (isGrounded && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)))
            {
                Jump();
            }
        }
        else if (CurrentMode == Mode.Ship)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                rb.velocity = new Vector2(rb.velocity.x, 12f); // Monte fortement à chaque clic
            }
        }

        Sprite.rotation = Quaternion.RotateTowards(Sprite.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }


    //-------------------
    //  METHODES PUBLIC
    //-------------------

    public void ToggleMode()
    {
        if (CurrentMode == Mode.Normal)
        {
            CurrentMode = Mode.Ship;
            rb.gravityScale = 4f;
        }
        else
        {
            CurrentMode = Mode.Normal;
            rb.gravityScale = 12.41067f;
        }
    }


    //-------------------
    //  METHODES PRIVEE
    //-------------------

    bool OnGround()
    {
        return Physics2D.OverlapCircle(GroundCheckTransform.position, GroundCheckRadius, GroundMask);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * 25, ForceMode2D.Impulse);
        RotateSprite();
    }

    void RotateSprite()
    {
        targetRotation *= Quaternion.Euler(0, 0, -90);
    }
}
