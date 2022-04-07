using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private GameObject pos1;
    [SerializeField] private GameObject pos2;
    [SerializeField] private GameObject pos3;
    [SerializeField] private GameObject pos4;
    [SerializeField] private GameObject pos5;
    [SerializeField] private GameObject pos6;
    private int movement = 0;
    private float step;
    private float turning_vel = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        step = Time.deltaTime * 3;
        if (movement == 0)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, pos2.transform.position, step);
            if (Vector3.Distance(this.transform.position, pos2.transform.position) <= 0.5f)
            {
                movement++;
            }
        }
        if (movement == 1)
        {
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, -90 , ref turning_vel , 0.1f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            this.transform.position = Vector3.MoveTowards(this.transform.position, pos3.transform.position, step);
            if (Vector3.Distance(this.transform.position, pos3.transform.position) <= 0.5f)
            {
                movement++;
            }
        }
        if (movement == 2)
        {
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, 0, ref turning_vel, 0.1f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            this.transform.position = Vector3.MoveTowards(this.transform.position, pos4.transform.position, step);
            if (Vector3.Distance(this.transform.position, pos4.transform.position) <= 0.5f)
            {
                movement++;
            }
        }
        if (movement == 3)
        {
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, 90, ref turning_vel, 0.1f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            this.transform.position = Vector3.MoveTowards(this.transform.position, pos5.transform.position, step);
            if (Vector3.Distance(this.transform.position, pos5.transform.position) <= 0.5f)
            {
                movement++;
            }
        }
        if (movement == 4)
        {
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, 0, ref turning_vel, 0.1f);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            this.transform.position = Vector3.MoveTowards(this.transform.position, pos6.transform.position, step);
            if (Vector3.Distance(this.transform.position, pos6.transform.position) <= 0.5f)
            {
                movement = 0;
                this.transform.position = pos1.transform.position;
            }
        }
    }
}
