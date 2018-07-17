using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour 
{
    private PlayerMove playerMove;
    private PlayerAttack playerAttack;
    private new Animation animation;

    void Start()
    {
        this.animation = GetComponent<Animation>();
        playerMove = GetComponent<PlayerMove>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    void LateUpdate()
    {
        if(playerAttack.state == PlayerState.ControlWalk)
        {
            if (playerMove.playerState == ControlWalkState.Idle)
            {
                PlayAnimation("Idle");
            }
            else if(playerMove.playerState == ControlWalkState.Run)
            {
                PlayAnimation("Run");
            }
        }
        else if(playerAttack.state == PlayerState.NormalAttack)
        {
            if(playerAttack.attackState == AttackState.Moving)
            {
                PlayAnimation("Run");
            }
        }
        else if(playerAttack.state == PlayerState.Death)
        {
            PlayAnimation("Death");
        }
    }

    void PlayAnimation(string name)
    {
        this.animation.CrossFade(name);
    }
}
