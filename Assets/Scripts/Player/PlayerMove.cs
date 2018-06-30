using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Run,Idle
}

public class PlayerMove : MonoBehaviour 
{
    public int speed = 4;
    public PlayerState playerState;
    public bool isMoving = false;
    private CharacterController characterController;
    private PlayerDir playerDir;

    void Start()
    {
        playerState = PlayerState.Idle;
        characterController = GetComponent<CharacterController>();
        playerDir = GetComponent<PlayerDir>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, playerDir.targetPosition);
        if (distance > 0.4f)
        {
            isMoving = true;
            characterController.SimpleMove(transform.forward * speed);
            playerState = PlayerState.Run;
        }
        else
        {
            isMoving = false;
            playerState = PlayerState.Idle;
        }
    }
}
