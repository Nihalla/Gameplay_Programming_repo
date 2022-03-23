using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineTrigger : MonoBehaviour
{
    private ThirdPersonMovement player_script;
    private GameObject player;
    private Collider player_collider;
    [SerializeField] private Camera main_cam;
    [SerializeField] private Camera spline_cam;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        player_collider = player.GetComponent<Collider>();
        player_script = player.GetComponent<ThirdPersonMovement>();
        spline_cam.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == player_collider)
        {
            player_script.in_spline = true;
            main_cam.enabled = false;
            spline_cam.enabled = true;
            player_script.cam = spline_cam;
        }
    }
}
