using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineMover : MonoBehaviour
{
    public Spline spline;
    public Transform follow_obj;
    [SerializeField] private GameObject looking_target;

    private Transform obj_transform;
    void Start()
    {
        obj_transform = transform;
    }

    void Update()
    {
        obj_transform.position = spline.whereOnSpline(follow_obj.position);
        transform.LookAt(looking_target.transform);
    }
}
