using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager TheMan;
    public float speed=5;
    float ConstSpeed;
    bool IsGrounded;
    public float XInput;
    public bool IsDead;
    float SlowDownTimestamp;
    public GameObject SlowDownEffect;
    CharacterController characterController;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Vector3 moveDirection = Vector3.zero;
    public Animator Anim;
    public float Multiplier = 1;
    // Start is called before the first frame update
    void Start()
    {
        ConstSpeed = speed;
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        AnimMan();
        Anim.enabled = !IsDead;
        if (IsDead || !TheMan.GameStarted || TheMan.GameTime >= TheMan.LevelTime)
            return;
        InputMan();
        if (SlowDownTimestamp > Time.time)
        {
            speed = ConstSpeed / 2;
            Multiplier = 0.5f;
        }
        else
        {
            speed = ConstSpeed;
            Multiplier = 1;
        }
    }
    void AnimMan()
    {
        if (XInput > 0) { 
        }
        else  if (XInput < 0)
        {
        }
        else
        {

        }
        Anim.SetFloat("Input", XInput*2);
        Anim.SetFloat("Multiplier", Multiplier);
        if (TheMan.GameTime >= TheMan.LevelTime) {
            Anim.SetBool("Finished", true);
        }
        else
        {
            Anim.SetBool("Finished", false);
        }
    }
    void InputMan()
    {
        if (TheMan.IsMobile)
        {
            if (Input.acceleration.x < 0.1f && Input.acceleration.x > -0.1f)
            {
                XInput = 0;

            }
            else
            {
                XInput = Input.acceleration.x;

            }

        }
        else
        {
            XInput = Input.GetAxis("Horizontal");

        }
    }
    public void SlowDown(float TheT)
    {
        SlowDownTimestamp = TheT + Time.time;
    }
    public void Damage()
    {
        if (TheMan.GameTime < TheMan.LevelTime)
        {
            IsDead = true;
        }
    }
    void Move()
    {
        if (IsDead || !TheMan.GameStarted || TheMan.GameTime >= TheMan.LevelTime)
        {
            XInput = 0;
        }
        IsGrounded = characterController.isGrounded;
        
        moveDirection = new Vector3(XInput*speed, 0.0f, 0);
       
        moveDirection = transform.TransformDirection(moveDirection);
        characterController.Move(new Vector3(moveDirection.x,-gravity, moveDirection.z) * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

    }

}
