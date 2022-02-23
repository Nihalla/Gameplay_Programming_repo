using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class FollowCam : MonoBehaviour
{
    PlayerControls controls;
    Vector2 move;
    private void Awake()
    {
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


    [SerializeField] private GameObject target;
    public Vector3 offset;

    /*private float mouseX;
    private float mouseY;
    private float mouseZ;*/

    private void Start()
    {
        offset = transform.position - target.transform.position;
    }

    private void LateUpdate()
    {
        Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime;
 
        float angleBetween = Vector3.Angle(Vector3.up, transform.forward);
        offset = Quaternion.Euler(0, -m.x * 100, 0) * offset;

        Vector3 LocalRight = target.transform.worldToLocalMatrix.MultiplyVector(transform.right);

        if (((angleBetween > 80.0f) && (m.y < 0)) || ((angleBetween < 160.0f) && (m.y > 0)))
        {
            offset = Quaternion.AngleAxis(m.y * 100, LocalRight) * offset;
        }
       
        float dist = Vector3.Distance(target.transform.position, transform.position);

        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = target.transform.position + offset;
        transform.LookAt(target.transform);

    }
}
