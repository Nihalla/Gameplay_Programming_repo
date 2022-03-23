using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spline : MonoBehaviour
{
    private Vector3[] spline_point;
    private int spline_count;

    public bool debug_drawspline = true;
    void Start()
    {
        spline_count = transform.childCount;
        spline_point = new Vector3[spline_count];

        for (int i = 0; i < spline_count; i++)
        {
            spline_point[i] = transform.GetChild(i).position;
        }
    }


    void Update()
    {
        spline_count = transform.childCount;
        spline_point = new Vector3[spline_count];

        for (int i = 0; i < spline_count; i++)
        {
            spline_point[i] = transform.GetChild(i).position;
        }

        for (int i = 0; i < spline_count; i++)
        {
            if (i + 1 < spline_count)
            {
                Debug.DrawLine(spline_point[i], spline_point[i + 1], Color.red);
            }
        }
    }

    public Vector3 whereOnSpline(Vector3 pos)
    {
        int closest_point = getClosestPoint(pos);

        if (closest_point == 0)
        {
            return splineSegment(spline_point[0], spline_point[1], pos);
        }    
        else if (closest_point == spline_count - 1)
        {
            return splineSegment(spline_point[spline_count -1], spline_point[spline_count - 2], pos);
        }
        else
        {
            Vector3 left_seg = splineSegment(spline_point[closest_point - 1], spline_point[closest_point], pos);
            Vector3 right_seg = splineSegment(spline_point[closest_point + 1], spline_point[closest_point], pos);

            if((pos - left_seg).sqrMagnitude <= (pos - right_seg).sqrMagnitude)
            {
                return left_seg;
            }
            else
            {
                return right_seg;
            }
        }
    }

    private int getClosestPoint(Vector3 pos)
    {
        int close_point = -1;
        float shortest_dist = 0.0f;

        for (int i = 0; i < spline_count; i++)
        {
            float sqr_dist = (spline_point[i] - pos).sqrMagnitude;
            if (shortest_dist == 0.0f || sqr_dist < shortest_dist)
            {
                shortest_dist = sqr_dist;
                close_point = i;
            }
        }

        return close_point;
    }

    public Vector3 splineSegment(Vector3 v1, Vector3 v2, Vector3 pos)
    {
        Vector3 v1_to_position = pos - v1;
        Vector3 seq_dir = (v2 - v1).normalized;

        float distance_to_v1 = Vector3.Dot(seq_dir, v1_to_position);

        if(distance_to_v1 < 0.0f)
        {
            return v1;
        }
        else if(distance_to_v1 * distance_to_v1 > (v2 - v1).sqrMagnitude)
        {
            return v2;
        }
        else
        {
            Vector3 from_v1 = seq_dir * distance_to_v1;
            return v1 + from_v1;
        }
    }
}
