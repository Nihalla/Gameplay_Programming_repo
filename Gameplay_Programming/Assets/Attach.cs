using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attach : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other == player.GetComponent<Collider>())
        {
            player.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == player.GetComponent<Collider>())
        {
            player.transform.parent = null;
        }
    }
}
