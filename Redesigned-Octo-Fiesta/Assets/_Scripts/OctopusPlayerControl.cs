using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusPlayerControl : MonoBehaviour
{
    enum PLAYER_STATE { S_WALK, S_IDLE, S_JUMP }; //player states
    PLAYER_STATE state;

    Rigidbody rb;
    private Vector3 startPosition;
    private Quaternion startRotation;
    Animator anim;
    CharacterController cc;
    bool previousIsGroundedValue;
    float rotateSpeed = 60f;
    float jumpForce = 0.35f;
    float gravityModifier = 0.2f;
    float yVelocity = 0;
    float speed = 10f;
    Ray camRay;
    RaycastHit hit;

    void Start()
    {
        anim = GetComponent<Animator>();
        cc = gameObject.GetComponent<CharacterController>();
        rb = gameObject.GetComponent<Rigidbody>();
        previousIsGroundedValue = cc.isGrounded;
        startPosition = transform.position;
        startRotation = transform.rotation;
        anim.SetTrigger("walk");
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
    private void Update()
    {
        var hAxis = Input.GetAxis("Horizontal"); //Get horizontal and vertical axis references
        var vAxis = Input.GetAxis("Vertical");

        aim(); //run aiming function
        if (Input.GetButtonDown("Fire1")) //run shooting function if press mouse button
        {
            shoot();
        }

        switch (state)
        {
            case PLAYER_STATE.S_IDLE: //idle state
                anim.SetTrigger("stop");
                if (hAxis != 0 || vAxis != 0) //switch to walk if moving
                {
                    state = PLAYER_STATE.S_WALK;
                    
                }
                if (Input.GetKeyDown(KeyCode.Space)) //switch to jump if press space
                {
                    state = PLAYER_STATE.S_JUMP;
                    
                }
                break;

            case PLAYER_STATE.S_WALK:
                anim.SetTrigger("walk");

                Vector3 forward = transform.TransformDirection(Vector3.forward) * vAxis; //moving around
                Vector3 right = transform.TransformDirection(Vector3.right) * hAxis;
                Vector3 direction = forward + right;
                cc.SimpleMove(direction * speed);

                if (hAxis == 0 && vAxis == 0) //if stop moving, go to idle
                {
                    state = PLAYER_STATE.S_IDLE;
                }
                break;

            case PLAYER_STATE.S_JUMP:
                anim.SetTrigger("jump");
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    state = PLAYER_STATE.S_IDLE;
                    if (Input.GetKeyDown(KeyCode.W))
                    {
                        state = PLAYER_STATE.S_WALK;
                    }
                }
                break;

        }
    }

    private void aim()
    {
        camRay = Camera.main.ScreenPointToRay(Input.mousePosition); //shoot ray at mouse
        if (Physics.Raycast(camRay, out hit))
        {
            transform.LookAt(hit.point); //look at whatever the ray hit
        }
    }
    
    private void shoot()
    {

    }
    
}


