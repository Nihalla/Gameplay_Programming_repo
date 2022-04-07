using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParenting : MonoBehaviour
{
    private GameObject player;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player)
        {
            player.transform.parent = this.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == player)
        {
            player.transform.parent = null;
        }
    }
}
