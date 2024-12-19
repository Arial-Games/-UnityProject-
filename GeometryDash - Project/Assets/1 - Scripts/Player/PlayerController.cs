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
public class PlayerController : MonoBehaviour
{
    public Speed CurrentSpeed;
    //                           0      1      2      3       4                            
    float[] speedValues = { 8.6f, 10.4f, 12.96f, 15.6f, 19.27f };

    public Transform GroundCheckTransform;
    public float GroundCheckRadius;
    public LayerMask GroundMask;

    public Transform Sprite;

    Rigidbody2D rb;


    //-------------------
    //  METHODES DEFAULT
    //-------------------

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.position += Vector3.right * speedValues[(int)CurrentSpeed] * Time.deltaTime;

        if (OnGround())
        {

            if (Input.GetMouseButton(0) || Input.GetKeyDown("space"))
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * 26.6581f, ForceMode2D.Impulse);
            }
        }
        else
        {
            Sprite.Rotate(Vector3.back * 5);
        }
    }


    //-------------------
    //  METHODES PUBLIC
    //-------------------


    //-------------------
    //  METHODES PRIVEE
    //-------------------

    bool OnGround()
    {
        return Physics2D.OverlapCircle(GroundCheckTransform.position, GroundCheckRadius, GroundMask);
    }
}