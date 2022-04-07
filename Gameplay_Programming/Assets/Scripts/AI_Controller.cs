using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Controller : MonoBehaviour
{
    public enum AI_States
    {
        Moving,
        Idle,
        Attacking,
        Targetting
    }

    private Camera cam;
    //public Animator animator;
    private float global_cooldown = 1f;

    [SerializeField] private int start_hp;
    [SerializeField] private int health = 3;
    [SerializeField] private GameObject sword;
    [SerializeField] private float i_frames = 4;

    [SerializeField] private Material m_normal;
    [SerializeField] private Material m_hit;

    //private GameObject parent;

    public AI_States AI_state;
    public NavMeshAgent agent;
    private GameObject player;
    [SerializeField] private Collider sight;
    [SerializeField] private bool player_in_sight = false;
    //private float min_dist = 3;
    //private float min_dist = 3;
    private float max_dist = 4;
    private float turning_speed = 1;
    private float movement_speed = 4;
    private float idle_time = 5;

    private Vector3 random_direction;
    private float walk_radius = 10;
    private Vector3 final_position;

    // Start is called before the first frame update
    private void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        AI_state = AI_States.Idle;
        player = GameObject.FindGameObjectWithTag("Player");
        cam = Camera.main;
        //parent = this.gameObject.transform.parent.gameObject;
        //animator = this.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {

        if (i_frames > 0)
        {
            i_frames -= Time.deltaTime;
            if (i_frames <= 0)
            {
                this.gameObject.GetComponent<MeshRenderer>().material = m_normal;
            }
        }

        if (global_cooldown >= 0)
        {
            global_cooldown -= Time.deltaTime;
        }

        if (!player.GetComponent<ThirdPersonMovement>().alive && this.gameObject.GetComponent<CapsuleCollider>().enabled)
        {
            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            player_in_sight = false;
            idle_time = 2f;
            if (AI_state == AI_States.Attacking || AI_state == AI_States.Targetting)
            {
                ChangeState(AI_States.Idle);
            }
        }
        if (player.GetComponent<ThirdPersonMovement>().alive && !this.gameObject.GetComponent<CapsuleCollider>().enabled)
        {
            this.gameObject.GetComponent<CapsuleCollider>().enabled = true;
        }

        switch (AI_state)
        {
            case AI_States.Moving:
                {
                    Move(final_position);
                    Turn(final_position);
                    if (Vector3.Distance(this.transform.position, final_position) <= 2)
                    {
                        idle_time = 5;
                        ChangeState(AI_States.Idle);
                    }
                    return;
                }

            case AI_States.Idle:
                {
                    if (idle_time >= 0)
                    {
                        idle_time -= Time.deltaTime;
                    }
                    else
                    {
                        random_direction = Random.insideUnitSphere * walk_radius;
                        random_direction += this.transform.position;
                        NavMeshHit hit;
                        NavMesh.SamplePosition(random_direction, out hit, walk_radius, 1);
                        final_position = hit.position;
                        ChangeState(AI_States.Moving);
                        //AI_state = AI_States.Moving;
                    }
                    return;
                }

            case AI_States.Attacking:
                {
                    Attack();
                    return;
                }

            case AI_States.Targetting:
                {
                    // MOVING

                    Move(player.transform.position);

                    /*float step = movement_speed * Time.deltaTime; 
                    this.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);*/


                    // TURNING

                    Turn(player.transform.position);

                    /*Vector3 targetDirection = player.transform.position - this.transform.position;

                    float singleStep = turning_speed * Time.deltaTime;

                    Vector3 newDirection = Vector3.RotateTowards(this.transform.forward, targetDirection, singleStep, 0.0f);
                    newDirection.y = Vector3.zero.y;
                   
                    this.transform.rotation = Quaternion.LookRotation(newDirection);*/
                    return;
                }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == player.GetComponent<Collider>() && player.GetComponent<ThirdPersonMovement>().alive)
        {
            player_in_sight = true;
            ChangeState(AI_States.Targetting);

            //AI_state = AI_States.Targetting;
        }
        else if (!player.GetComponent<ThirdPersonMovement>().alive && this.gameObject.GetComponent<CapsuleCollider>().enabled)
        {
            this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
            player_in_sight = false;
            idle_time = 2f;
            ChangeState(AI_States.Idle);
            
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if(other == player.GetComponent<Collider>() && player.GetComponent<ThirdPersonMovement>().alive)
        {
            if (Vector3.Distance(this.transform.position, player.transform.position) <= max_dist &&
                player_in_sight)
            {
                ChangeState(AI_States.Attacking);
                //AI_state = AI_States.Attacking;
            }
            else if (!player.GetComponent<ThirdPersonMovement>().alive && this.gameObject.GetComponent<CapsuleCollider>().enabled)
            {
                Debug.Log("Player dead, back to idle");
                player_in_sight = false;
                idle_time = 2f;
                this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
                ChangeState(AI_States.Idle);
            }
            else
            {
                ChangeState(AI_States.Targetting);
                //AI_state = AI_States.Targetting;
            }
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == player.GetComponent<Collider>())
        {
            player_in_sight = false;
            idle_time = 5;
            ChangeState(AI_States.Idle);
            //AI_state = AI_States.Idle;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision);
        if (collision.gameObject.tag.Equals("Weapon") && i_frames <= 0)
        {
            takeDamage();
            i_frames = 2;
            this.gameObject.GetComponent<MeshRenderer>().material = m_hit;
        }
        //if(collision == sword.GetComponent<Collision>() && i_frames <= 0)
        //{
        //    takeDamage();
        //    i_frames = 2;
        //}
    }

    private void ChangeState(AI_States new_state)
    {
        AI_state = new_state;
    }

    private void Move(Vector3 target)
    {
        float step = movement_speed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(this.transform.position, target, step);
    }

    private void Turn(Vector3 target)
    {
        Vector3 targetDirection = target - this.transform.position;

        float singleStep = turning_speed * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(this.transform.forward, targetDirection, singleStep, 0.0f);
        newDirection.y = Vector3.zero.y;

        this.transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void takeDamage()
    {
        health -= 1;
        if (health == 0)
        {
            start_hp -= 1;
            max_dist /= 2;
            if (start_hp > 0)
            {
                i_frames = 2;
                this.gameObject.GetComponent<MeshRenderer>().material = m_hit;

                health = start_hp;
                this.transform.localScale /= 2;

                Vector3 rand_in_range = Random.insideUnitSphere * 3;
                rand_in_range += this.transform.position;
                Instantiate(this, new Vector3(rand_in_range.x, this.transform.position.y, rand_in_range.z), Quaternion.identity);
                rand_in_range = Random.insideUnitSphere * 3;
                rand_in_range += this.transform.position;
                Instantiate(this, new Vector3(rand_in_range.x, this.transform.position.y, rand_in_range.z), Quaternion.identity);
            }
            Destroy(this.gameObject, 0);
            cam.GetComponent<FollowCam>().Unlock();
            //Spawn in two new cubes
            //Despawn main cube

        }
    }

    private void Attack()
    {
        if (global_cooldown <= 0 && player.GetComponent<ThirdPersonMovement>().alive)
        {
            //anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 1);
            //animator.SetTrigger("SlimeAttack");
            //animator.Play("Slime_Attack_Anim");
            //weapon.GetComponent<BoxCollider>().isTrigger = false;
            //cutscene_script.ButtonPress();

            //parent.transform.position.z = parent.transform.position.z - 1f;

           

            //this.gameObject.GetComponent<Rigidbody>().AddForce(0f, 0f, -1000f);
            this.gameObject.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, -1000f));
            player.GetComponent<ThirdPersonMovement>().takeDMG();
            //anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 0);
            //weapon.GetComponent<BoxCollider>().isTrigger = true;
            global_cooldown = 1f;
        }
    }
}
