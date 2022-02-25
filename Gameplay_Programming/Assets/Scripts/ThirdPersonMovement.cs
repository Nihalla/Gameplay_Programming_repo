using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Camera cam;

    [SerializeField] private int health = 10;
    [SerializeField] private float speed = 6f;
    [SerializeField] private float turning_time = 0.1f;
    private float speed_boost = 1f;
    private float turning_vel;
    private Vector3 vel;

    private bool grounded;
    [SerializeField] private float ground_dist;
    [SerializeField] private LayerMask ground_mask;
    [SerializeField] private float jump_height;
    [SerializeField] private float gravity;

    public bool double_jump = false;
    private bool can_jump = true;
    public bool sprint_power = false;
    private bool rolling = false;

    [SerializeField] private GameObject jump_p;
    [SerializeField] private GameObject sprint_p;


    private float global_cooldown = 1f;
    public float jump_boost_timer = 10.0f;
    public float speed_boost_timer = 10.0f;
    private float fall_timer = 0f;
    private int fall_damage = 5;

    private Animator anim;
    [SerializeField] private GameObject weapon;
    [SerializeField] private GameObject text_canvas;


    PlayerControls controls;
    Vector2 move;
    Vector2 cam_move;

    public Vector3 offset;
    private Vector3 direction;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.PlayerJump.performed += ctx => Jump();
        controls.Gameplay.PickUpItem.performed += ctx => pickUpWeapon();
        controls.Gameplay.PlayerAttack.performed += ctx => StartCoroutine(Attack());
        controls.Gameplay.PlayerRoll.performed += ctx => Roll(direction);

        controls.Gameplay.PlayerMove.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.PlayerMove.canceled += ctx => move = Vector2.zero;

        controls.Gameplay.CameraMove.performed += ctx => cam_move = ctx.ReadValue<Vector2>();
        controls.Gameplay.CameraMove.canceled += ctx => cam_move = Vector2.zero;
        
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
        offset = cam.transform.position - transform.position;
    }
    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }
    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    // Update is called once per frame 
    void Update()
    {
        if (double_jump)
        {
            jump_p.SetActive(true);
            jump_boost_timer -= Time.deltaTime;
            if (jump_boost_timer <= 0.0f)
            {
                jumpTimer();
                jump_p.SetActive(false);

            }
        }
        if (sprint_power)
        {
            sprint_p.SetActive(true);
            speed_boost = 2f;
            speed_boost_timer -= Time.deltaTime;
            if (speed_boost_timer <= 0.0f)
            {
                sprintTimer();
                sprint_p.SetActive(false);
            }
        }

        if (global_cooldown >= 0 )
        {
            global_cooldown -= Time.deltaTime;
        }
        grounded = Physics.CheckSphere(transform.position, ground_dist, ground_mask);
        if (grounded)
        {
            can_jump = true;
            if (vel.y < 0)
            {
                anim.SetBool("Grounded", true);
                StartCoroutine(jumpDelay());
                if (fall_timer < 0)
                {
                    int dmg_x = Mathf.Abs((int)fall_timer);
                    if (dmg_x == 0)
                    {
                        dmg_x = 1;
                    }

                    health -= fall_damage * dmg_x;
                }
                fall_timer = 0f;
                //anim.SetLayerWeight(anim.GetLayerIndex("Jump Layer"), 0);
            }

        }
        else
        { 
            anim.SetBool("Grounded", false);
            fall_timer -= Time.deltaTime * 2f;
        }


        Vector3 dir = new Vector3(move.x, 0f, move.y).normalized;
        direction = dir;
        if (!rolling)
        {
            Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime;
            //Vector2 cam_m = new Vector2(cam_move.x, cam_move.y) * Time.deltaTime;
            //offset = Quaternion.Euler(0, -cam_m.x * 100, 0) * offset;

            transform.Translate(dir, Space.World);

            if (dir.magnitude >= 0.1f)
            {
                float move_angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, move_angle, ref turning_vel, turning_time);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }

            if (grounded && vel.y < 0)
            {
                vel.y = -2f;
            }



            if (dir.x < 0.1 && dir.x > -0.1 && dir.z < 0.1 && dir.z > -0.1)
            {
                anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
                speed = 6f;
            }
            else if (move.x > 0.8 || move.y > 0.8 || move.x < -0.8 || move.y < -0.8)
            {
                anim.SetFloat("Speed", 1f, 0.1f, Time.deltaTime);
                speed = 6f;
            }
            else
            {
                if (sprint_power)
                {
                    anim.SetFloat("Speed", 1f, 0.1f, Time.deltaTime);
                    speed = 6f;
                }
                else
                {
                    anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
                    speed = 3f;
                }
            }


            controller.Move(dir * speed * speed_boost * Time.deltaTime);


            vel.y += gravity * Time.deltaTime;
            controller.Move(vel * Time.deltaTime);
        }
        //transform.rotation.Set(transform.rotation.x, cam.transform.rotation.y, transform.rotation.z, transform.rotation.w);

    }

    private void Jump()
    {
       
        if (grounded && can_jump)
        {
            //anim.SetLayerWeight(anim.GetLayerIndex("Jump Layer"), 1);
            anim.SetBool("Grounded", false);
            anim.SetTrigger("Jump");
            //Debug.Log("I jump");
            

            vel.y = Mathf.Sqrt(jump_height * -2 * gravity);
            fall_timer += 4f;
        }
        else if (!grounded && can_jump && double_jump)
        {

            //anim.SetLayerWeight(anim.GetLayerIndex("Jump Layer"), 1);
            //anim.SetTrigger("Jump");
            anim.SetBool("Grounded", false);
            anim.SetBool("SecondJump", true);
            //Debug.Log(anim.GetBool("SecondJump"));
            

            vel.y = Mathf.Sqrt(jump_height * -2 * gravity);
            can_jump = false;
        }
        //yield return new WaitForSeconds(2.0f);

    }

    void jumpTimer()
    {
        double_jump = false;
    }

    void sprintTimer()
    {
        sprint_power = false;
        speed_boost = 1f;
    }

    private IEnumerator Attack()
    {
        if (global_cooldown <= 0)
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 1);
            anim.SetTrigger("Attack");

            yield return new WaitForSeconds(1f);
            anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 0);
            global_cooldown = 1f;
        }
    }

    private void Roll(Vector3 dir)
    {
        if (global_cooldown <= 0)
        {
            anim.SetTrigger("Roll");
            controller.Move(dir * speed * 2 * Time.deltaTime);
            global_cooldown = 1f;
            rolling = true;
            StartCoroutine(secDelay());
        }
    }

    private IEnumerator jumpDelay()
    {
        yield return new WaitForSeconds(0.5f);

        anim.SetBool("SecondJump", false);
    }

    private IEnumerator secDelay()
    {
        yield return new WaitForSeconds(1f);
        rolling = false;
    }

    private void pickUpWeapon()
    {
        if (text_canvas.activeSelf)
        {
            GameObject.FindWithTag("Weapon").GetComponent<PickUp>().delete = true;
            anim.SetBool("Armed", true);
            weapon.SetActive(true);
        }
    }

}
