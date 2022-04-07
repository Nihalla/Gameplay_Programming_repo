using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attach : MonoBehaviour
{
    private GameObject player;
    private Transform temp;
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
            temp = player.transform;
            player.transform.rotation = this.transform.rotation;
            player.transform.SetParent(this.transform, true);
            player.transform.rotation = temp.rotation;
            //player.transform.localScale = 1 / this.transform.localScale;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == player.GetComponent<Collider>())
        {
            player.transform.SetParent(null); 
        }
    }
}
