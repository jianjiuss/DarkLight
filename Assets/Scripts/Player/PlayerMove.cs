using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ControlWalkState
{
    Run,Idle
}

public class PlayerMove : MonoBehaviour 
{
    public int speed = 4;
    public ControlWalkState playerState;
    public bool isMoving = false;
    private CharacterController characterController;
    private PlayerDir playerDir;
    private PlayerAttack playerAttack;

    void Start()
    {
        playerState = ControlWalkState.Idle;
        characterController = GetComponent<CharacterController>();
        playerDir = GetComponent<PlayerDir>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    void Update()
    {
        if(playerAttack.state != PlayerState.ControlWalk)
        {
            return;
        }
        float distance = Vector3.Distance(transform.position, playerDir.targetPosition);
        if (distance > 0.4f)
        {
            isMoving = true;
            characterController.SimpleMove(transform.forward * speed);
            playerState = ControlWalkState.Run;
        }
        else
        {
            isMoving = false;
            playerState = ControlWalkState.Idle;
        }
    }

    public void SimpleMove(Vector3 targetPos)
    {
        transform.LookAt(targetPos);
        characterController.SimpleMove(transform.forward * speed);
    }
}
