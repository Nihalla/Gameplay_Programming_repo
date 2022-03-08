using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Cutscene : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private GameObject button;
    [SerializeField] private GameObject door;
    [SerializeField] private Camera main_cam;
    [SerializeField] private Camera cinematic_cam;
    private ThirdPersonMovement player_script;
    private Collider player_collider;
    private bool button_pressed;
    private bool in_collider;
    private bool move_back = false;
    private bool move_cam = false;
    private bool move_cam2 = false;
    private bool open_door = false;
    [SerializeField] private Transform stop1;
    private Transform stop2;

    //[SerializeField] private

    void Awake()
    {
        cinematic_cam.enabled = false;
        player = GameObject.FindWithTag("Player");
        player_collider = player.GetComponent<Collider>();
        player_script = player.GetComponent<ThirdPersonMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == player_collider)
        {
            main_cam.enabled = false;
            cinematic_cam.enabled = true;
            in_collider = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        float step = Time.deltaTime * 0.5f;
        stop2 = main_cam.transform;
        
        if (button_pressed)
        {
            //Debug.Log("Button was pressed");
            player_script.in_cinematic = true;
            player.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            main_cam.transform.rotation = Quaternion.Euler(main_cam.transform.rotation.x, 90f, main_cam.transform.rotation.z);

            if (button.transform.position.x < 5.9f && !move_back)
            {
                button.transform.position = Vector3.MoveTowards(button.transform.position,
                    new Vector3(button.transform.position.x + 0.14f, button.transform.position.y, button.transform.position.z), step);
            }
            if (button.transform.position.x >= 5.9f)
            {
                move_back = true;
            }
            if (button.transform.position.x > 5.76f &&move_back)
            {
                button.transform.position = Vector3.MoveTowards(button.transform.position,
                    new Vector3(button.transform.position.x - 0.14f, button.transform.position.y, button.transform.position.z), step);
                move_cam = true;
            }

            if (move_cam)
            {
                cinematic_cam.transform.position = Vector3.MoveTowards(cinematic_cam.transform.position, stop1.position, step * 3);
                
                cinematic_cam.transform.rotation = Quaternion.Euler(stop1.rotation.eulerAngles);
                if (cinematic_cam.transform.position == stop1.position)
                {
                    move_cam = false;
                    open_door = true;
                }
            }

            if (open_door && door.transform.position.z > -2.11f)
            {
                door.transform.position = Vector3.MoveTowards(door.transform.position, new Vector3(door.transform.position.x, door.transform.position.y, -2.5f), step * 3);
            }
            else if (open_door && door.transform.position.z <= -2.11f)
            {
                move_cam2 = true;
            }

            if (move_cam2)
            {
                cinematic_cam.transform.position = Vector3.MoveTowards(cinematic_cam.transform.position, stop2.position, step * 6f);
                cinematic_cam.transform.rotation = stop2.rotation;
                if (cinematic_cam.transform.position == stop2.position)
                {
                    cinematic_cam.enabled = false;
                    main_cam.enabled = true;
                    player_script.in_cinematic = false;
                    move_cam2 = false;
                    Destroy(this, 0f);
                }
            }

        }

       /* float step = Time.deltaTime * speed;
        exit.x = exit_cam.transform.position.x;
        exit.y = player.transform.position.y + 0.707f;
        exit.z = player.transform.position.z;
        exit_cam_pos.transform.position = exit;
        exit_cam.transform.position = Vector3.MoveTowards(exit_cam.transform.position, exit_cam_pos.transform.position, step);*/
    }

    private void OnTriggerExit(Collider other)
    {
        if(other == player_collider)
        {
            cinematic_cam.enabled = false;
            main_cam.enabled = true;
            in_collider = false;
        }
    }

    public void ButtonPress()
    {
        if (!button_pressed && in_collider)
        {
            button_pressed = true;
        }
    }
}