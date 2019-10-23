using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum PLAYER_STATE { S_WALK, S_SHOOT, S_IDLE, S_JUMP} //add or remove states here
    PLAYER_STATE state;
    Animator anim;
    Rigidbody rb;

    void Start()
    {
        state = PLAYER_STATE.S_IDLE; //set initial state to idle
        anim = gameObject.GetComponent<Animator>(); //get animator and rigidbody
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        switch (state)
        {
            case PLAYER_STATE.S_IDLE: //idle state
                break;
            case PLAYER_STATE.S_JUMP: //jump state
                break;
            case PLAYER_STATE.S_SHOOT: //shoot state
                break;
            case PLAYER_STATE.S_WALK: //walk state
                break;
        }
    }
}
