using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Speed{
    Slow = 0,
    normale = 1,
    Fast = 2,
    VeryFast = 3,
    SuperFast = 4,
}
public class PlayerController : MonoBehaviour
{
    public Speed CurrentSpeed;
//                           0      1      2      3       4                            
    float[] speedValues = { 8.6f, 10.4f, 12.96f, 15.6f, 19.27f};

    public Transform GroundCheckTransform;
    public float GroundCheckRadius;
    public LayerMask GroundMask;

    Rigidbody2D rb;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.position += Vector3.right * speedValues[(int)CurrentSpeed] * Time.deltaTime;

        if (Input.GetMouseButton(0) || Input.GetKeyDown("space")){
            
            if(OnGround()){
                rb.AddForce(Vector2.up * 26.6581f, ForceMode2D.Impulse);
            }

        }
    }

    bool OnGround(){
        return Physics2D.OverlapCircle(GroundCheckTransform.position,GroundCheckRadius,GroundMask);
    }
}
