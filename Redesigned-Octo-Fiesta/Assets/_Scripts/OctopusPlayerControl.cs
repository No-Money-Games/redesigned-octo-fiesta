using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusPlayerControl : MonoBehaviour
{
    enum PLAYER_STATE { S_WALK, S_IDLE, S_RUN, S_JUMP };
    PLAYER_STATE state;
    private Vector3 startPosition;
    private Quaternion startRotation;
    Animator anim;
    CharacterController cc;
    bool previousIsGroundedValue;
    float rotateSpeed = 60f;
    float jumpForce = 0.35f;
    float gravityModifier = 0.2f;
    float yVelocity = 0;
    float speed = 10000f;
    void Start()
    {
        state = PLAYER_STATE.S_IDLE;
        anim = GetComponent<Animator>();
        cc = gameObject.GetComponent<CharacterController>();
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        previousIsGroundedValue = cc.isGrounded;
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state == PLAYER_STATE.S_WALK)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                state = PLAYER_STATE.S_JUMP;
                anim.SetTrigger("jump");
            }
        }
    }
    void Update()
    {

        //float hAxis = Input.GetAxis("Horizontal");
        //float vAxis = Input.GetAxis("Vertical");
        //transform.Rotate(0, hAxis * rotateSpeed * Time.deltaTime, 0);
        //transform.position = new Vector3(+Time.deltaTime*speed,transform.position.y,transform.position.z);




        switch (state)
        {
            case PLAYER_STATE.S_IDLE:
                if (Input.GetKeyDown(KeyCode.W))
                {
                    state = PLAYER_STATE.S_WALK;
                    anim.SetTrigger("walk");
                    if (Input.GetKey(KeyCode.W))
                    {
                        transform.Translate(Vector3.up * Time.deltaTime * 1);
                        Debug.Log("move");
                    }
                    if (Input.GetKey(KeyCode.S))
                    {
                        transform.position -= transform.forward * speed * Time.deltaTime;
                    }
                    if (Input.GetKey(KeyCode.D))
                    {
                        transform.position += transform.right * speed * Time.deltaTime;
                    }
                    if (Input.GetKey(KeyCode.A))
                    {
                        transform.position -= transform.right * speed * Time.deltaTime;
                    }
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    state = PLAYER_STATE.S_JUMP;
                    anim.SetTrigger("jump");
                    
                }
                break;

            case PLAYER_STATE.S_WALK:
                if (Input.GetKeyUp(KeyCode.W))
                {
                    state = PLAYER_STATE.S_IDLE;
                    anim.SetTrigger("stop");
                }
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    state = PLAYER_STATE.S_RUN;
                    anim.SetTrigger("run");
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    state = PLAYER_STATE.S_JUMP;
                    anim.SetTrigger("jump");
                    
                }
                break;

            case PLAYER_STATE.S_JUMP:
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    state = PLAYER_STATE.S_IDLE;
                    anim.SetTrigger("stop");
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        state = PLAYER_STATE.S_WALK;
                        anim.SetTrigger("walk");
                    }
                }
                break;

        }
    }
}


