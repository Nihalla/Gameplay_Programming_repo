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
    private Vector3 mov;

    private bool grounded;
    private bool alive = true;
    private bool has_weapon = false;
    private bool weapon_sheathed = true;
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
    [SerializeField] private float fall_timer = 0f;
    private int fall_damage = 5;

    private Animator anim;
    [SerializeField] private GameObject weapon;
    [SerializeField] private GameObject idle_weapon;

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
        controls.Gameplay.PickUpItem.performed += ctx => weaponAction();
        controls.Gameplay.PlayerAttack.performed += ctx => StartCoroutine(Attack());
        controls.Gameplay.PlayerRoll.performed += ctx => Roll(direction);
        controls.Gameplay.PlayerRevive.performed += ctx => Revive();

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
        if (health <=0)
        {
            alive = false;
            anim.SetBool("Alive", false);
        }
        if (alive)
        {
            if (double_jump)
            {
                jump_p.SetActive(true);
                var ps = jump_p.GetComponent<ParticleSystem>();
                var pMain = ps.main;
                pMain.startSize = new ParticleSystem.MinMaxCurve(0.01f, jump_p.GetComponent<ParticleSystem>().main.startSize.constantMax - 0.05f * Time.deltaTime);
                jump_boost_timer -= Time.deltaTime;
                if (jump_boost_timer <= 0.0f)
                {
                    jumpTimer();
                    pMain.startSize = new ParticleSystem.MinMaxCurve(0.025f, 0.5f);
                    jump_p.SetActive(false);

                }
            }
            if (sprint_power)
            {
                sprint_p.SetActive(true);
                var ps = sprint_p.GetComponent<ParticleSystem>();
                var pMain = ps.main;
                pMain.startSize = new ParticleSystem.MinMaxCurve(0.01f, sprint_p.GetComponent<ParticleSystem>().main.startSize.constantMax - 0.025f * Time.deltaTime);
                speed_boost = 2f;
                speed_boost_timer -= Time.deltaTime;
                if (speed_boost_timer <= 0.0f)
                {
                    sprintTimer();
                    pMain.startSize = new ParticleSystem.MinMaxCurve(0.01f, 0.25f);
                    sprint_p.SetActive(false);
                }
            }

            if (global_cooldown >= 0)
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
                    fall_timer = 1f;
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

                

                if (dir.magnitude >= 0.1f)
                {
                    float move_angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.GetComponent<Transform>().eulerAngles.y;
                    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, move_angle, ref turning_vel, turning_time);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);
                    Vector3 camForward = Quaternion.Euler(0f, move_angle, 0f).normalized * Vector3.forward;
                    //camForward = camForward;
                    mov = new Vector3(camForward.x, 0f, camForward.z);
                    controller.Move(camForward * speed * speed_boost * Time.deltaTime);
                    //transform.Translate(camForward, Space.World);
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

                

                


                vel.y += gravity * Time.deltaTime;
                vel.x = dir.x;
                vel.z = dir.z;
                controller.Move(vel * Time.deltaTime);
            }
            //transform.rotation.Set(transform.rotation.x, cam.transform.rotation.y, transform.rotation.z, transform.rotation.w);
        }
    }

    private void Jump()
    {
       
        if (grounded && can_jump && alive)
        {
            //anim.SetLayerWeight(anim.GetLayerIndex("Jump Layer"), 1);
            anim.SetBool("Grounded", false);
            anim.SetTrigger("Jump");
            //Debug.Log("I jump");
            

            vel.y = Mathf.Sqrt(jump_height * -2 * gravity);
            fall_timer += 4f;
        }
        else if (!grounded && can_jump && double_jump && alive)
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

    private void Revive()
    {
        if(!alive)
        {
            anim.SetBool("Alive", true);
            anim.SetTrigger("Revive");
            health = 10;
            alive = true;
            
            StartCoroutine(secDelay());
            fall_timer = 0f;
        }
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
        if (global_cooldown <= 0 && alive)
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
        if (global_cooldown <= 0 && alive)
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

    private void weaponAction()
    {
        if (text_canvas.activeSelf && !has_weapon && alive)
        {
            GameObject.FindWithTag("Weapon").GetComponent<PickUp>().delete = true;
            //anim.SetBool("Armed", true);
            idle_weapon.SetActive(true);
            has_weapon = true;
        }
        else if (has_weapon && !weapon_sheathed && alive)
        {
            anim.SetTrigger("Sheathe");
            StartCoroutine(secDelay());
            weapon.SetActive(false);
            idle_weapon.SetActive(true);
            anim.SetBool("Armed", false);
            weapon_sheathed = true;
            

        }
        else if (has_weapon && weapon_sheathed && alive)
        {
            anim.SetTrigger("Unsheathe");
            StartCoroutine(secDelay());
            idle_weapon.SetActive(false);
            weapon.SetActive(true);
            anim.SetBool("Armed", true);
            weapon_sheathed = false;
        }
    }

}
