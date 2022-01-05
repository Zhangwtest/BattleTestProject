using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;

    void LateUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            if (transform.position != player.transform.position)
            {
                transform.position = new Vector3(player.transform.position.x + 2f, 0.62f, -0.85f);
            }
        }
    }
}
