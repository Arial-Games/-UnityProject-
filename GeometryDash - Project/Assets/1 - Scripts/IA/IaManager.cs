using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IaManager : MonoBehaviour
{
    GameObject player;

    private void Start()
    {
        StartCoroutine(WaitForSeconds(0.2f));
    }

    IEnumerator WaitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (player == null)
            player = GameObject.Find("Basic-player(Clone)");

        player.GetComponent<AIController>().enabled = true;
    }
}
