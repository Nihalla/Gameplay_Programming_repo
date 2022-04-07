using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class FollowCam : MonoBehaviour
{
    PlayerControls controls;
    private GameObject player;

    Vector2 move;
    private GameObject obj;
    private bool locked = false;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");

        controls = new PlayerControls();
        
        controls.Gameplay.CameraMove.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.CameraMove.canceled += ctx => move = Vector2.zero;
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }


    [SerializeField] private GameObject follow_target;
    [SerializeField] private GameObject looking_target;
    [SerializeField] private Transform looking_transform;
    public Vector3 offset;

    /*private float mouseX;
    private float mouseY;
    private float mouseZ;*/

    private void Start()
    {
        offset = transform.position - follow_target.transform.position;
        looking_transform = follow_target.transform;
    }

    private void LateUpdate()
    {
        if (locked)
        {
            //Vector3 dir = (follow_target.transform.position - obj.transform.position).normalized;
            Vector3 midPoint = (follow_target.transform.position + obj.transform.position) / 2f;
            looking_target.transform.position = midPoint;
            looking_transform = looking_target.transform;
            //Debug.Log(transform.rotation.x);
            if (transform.rotation.x >= 0.15f && player.GetComponent<ThirdPersonMovement>().grounded)
            {
                looking_transform = follow_target.transform;
                locked = false;
            }
            /*if (obj == null)
            {
                looking_transform = follow_target.transform;
                locked = false;
            }*/
        }
        Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime;
 
        float angleBetween = Vector3.Angle(Vector3.up, transform.forward);
        offset = Quaternion.Euler(0, -m.x * 100, 0) * offset;

        Vector3 LocalRight = follow_target.transform.worldToLocalMatrix.MultiplyVector(transform.right);

        if (((angleBetween > 90.0f) && (m.y < 0)) || ((angleBetween < 160.0f) && (m.y > 0)))
        {
            offset = Quaternion.AngleAxis(m.y * 100, LocalRight) * offset;
        }
       
        float dist = Vector3.Distance(follow_target.transform.position, transform.position);

        float desiredAngle = follow_target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = follow_target.transform.position + offset;
        transform.LookAt(looking_transform);

    }

    public void Lock()
    { 
        Ray lock_on_ray = new Ray(transform.position, transform.forward);
        if (!locked)
        {
            RaycastHit hit;
            if (Physics.Raycast(lock_on_ray, out hit, 100f))
            {
                //Debug.Log(hit.collider.tag);
                if (hit.collider.tag == "Lookable")
                {
                    looking_transform = hit.transform;
                    obj = hit.collider.gameObject;
                    locked = true;

                }
            }
        }
        else
        {
            looking_transform = follow_target.transform;
            locked = false;
        }
    }

    public void Unlock()
    {
        looking_transform = follow_target.transform;
        locked = false;
    }
}
