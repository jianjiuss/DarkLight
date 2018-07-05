using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour 
{
    private PlayerMove playerMove;
    private new Animation animation;

    void Start()
    {
        this.animation = GetComponent<Animation>();
        playerMove = GetComponent<PlayerMove>();
    }

    void LateUpdate()
    {
        if (playerMove.playerState == PlayerState.Idle)
        {
            PlayAnimation("Idle");
        }
        else if(playerMove.playerState == PlayerState.Run)
        {
            PlayAnimation("Run");
        }
    }

    void PlayAnimation(string name)
    {
        this.animation.CrossFade(name);
    }
}
