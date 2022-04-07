using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform pos1;
    [SerializeField] private Transform pos2;
    [SerializeField] private Transform pos3;
    [SerializeField] private Transform pos4;

    private float interpolation_float;
    [SerializeField] private float time;
    private float step;
    public bool forward = true;
    private float movementDirection = 1.0f;
    private float pause_timer = 5.0f;
    private bool can_move = true;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Current position" + this.transform.position);
        //Debug.Log("Connection to fourth point" + pos4.position);

        step = 1 / time;

        if (Vector3.Distance(transform.position, pos1.position) <= step)
        {
            forward = true;
            movementDirection = 1.0f;
        }
        if (Vector3.Distance(transform.position, pos4.position) <= step)
        {
            movementDirection = -1.0f;
            forward = false;
            //can_move = false;
        }

        /*if (Vector3.Distance(transform.position, pos1.position) <= step)
        {

            //can_move = false;
            forward = true;
            movementDirection = 1.0f;
            if (Vector3.Distance(transform.position, pos1.position) <= 0.5f && (can_move))
            {
                time = 6.0f;
            }
            //can_move = true;
            //Debug.Log(Vector3.Distance(transform.position, pos1.position));
        }
        if (Vector3.Distance(transform.position, pos4.position) <= step)
        {
                //can_move = false;
            movementDirection = -1.0f;
            forward = false;
            if (Vector3.Distance(transform.position, pos1.position) <= 0.5f && (!can_move))
            {
                time = 6.0f;
            }
            //can_move = true;
            //Debug.Log("Connection to fourth point" + pos4.position);
        }*/
        if (can_move)
        {
            interpolation_float = (interpolation_float + movementDirection * Time.deltaTime) % time;

            /*if(forward)
            {
                this.transform.position = CubeLerp(pos1.position, pos2.position, pos3.position, pos4.position, interpolation_float * step);
            }
            else
            {
                this.transform.position = CubeLerp(pos4.position, pos3.position, pos2.position, pos1.position, interpolation_float * step);
            }*/

            this.transform.position = CubeLerp(pos1.position, pos2.position, pos3.position, pos4.position, interpolation_float * step);
        }
        else
        {
            timer();
        }
        /*pause_timer -= Time.deltaTime;
        if (pause_timer <= 0)
        {
            //can_move = true;
            pause_timer = 0.5f;
            time = 2.0f;

        }  */


    }
    private Vector3 QuadLerp(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        Vector3 ab = Vector3.Slerp(a, b, t);
        Vector3 bc = Vector3.Slerp(b, c, t);

        return Vector3.Slerp(ab, bc, interpolation_float * step);
    }

    private Vector3 CubeLerp(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        Vector3 ab_bc = QuadLerp(a, b, c, t);
        Vector3 bc_cd = QuadLerp(b, c, d, t);

        return Vector3.Slerp(ab_bc, bc_cd, interpolation_float * step);
    }

    private void timer()
    {
        pause_timer -= Time.deltaTime;
        if (pause_timer <= 0)
        {
            this.transform.position = pos1.position;
            can_move = true;
            pause_timer = 5.0f;
            
        }
    }
}
