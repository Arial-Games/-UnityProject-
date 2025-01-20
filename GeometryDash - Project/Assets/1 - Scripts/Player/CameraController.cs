
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Player Settings")]
    public Transform player;

    // Camera Settings
    float followSpeed, offsetX = 5f, fixedY = 2.3f;

    [Header("Smoothing")]
    public float smoothTime = 0.3f;
    Vector3 velocity = Vector3.zero;



    //-------------------
    //  METHODES DEFAULT
    //-------------------


    //-------------------
    //  METHODES PUBLIC
    //-------------------


    //-------------------
    //  METHODES PRIVEE
    //-------------------

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 targetPosition = new Vector3(player.position.x + offsetX, fixedY, transform.position.z);

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
