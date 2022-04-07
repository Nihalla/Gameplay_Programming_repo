using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    private GameObject[] agents;
    private Vector3 random_direction;
    [SerializeField] float walk_radius;
    

    // Start is called before the first frame update
    void Start()
    {
        agents = GameObject.FindGameObjectsWithTag("AI");
    }

    // Update is called once per frame
    void Update()
    {
        //random_direction = Random.insideUnitSphere * walk_radius;
    } 
}
