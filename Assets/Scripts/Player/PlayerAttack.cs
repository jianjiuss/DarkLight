using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    ControlWalk,
    NormalAttack,
    SkillAttack
}

public enum AttackState
{
    Moving,
    Idle
}

public class PlayerAttack : MonoBehaviour {

    public PlayerState state = PlayerState.ControlWalk;
    public AttackState attackState = AttackState.Idle;

    public string aniname_normalattack;
    public float time_normalattack;
    public float minDistance = 5;
    public float normalAttackDamage = 10;

    private Transform targetTransform;
    private float timer = 0;
    private PlayerMove playerMove;

    void Awake()
    {
        playerMove = GetComponent<PlayerMove>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit) && raycastHit.collider.tag.Equals(Tags.enemy))
            {
                state = PlayerState.NormalAttack;
                targetTransform = raycastHit.collider.gameObject.transform;
            }
            else
            {
                targetTransform = null;
                state = PlayerState.ControlWalk;
            }
        }

        if(state == PlayerState.NormalAttack)
        {
            float distance = Vector3.Distance(transform.position, targetTransform.position);
            if(distance <= minDistance)
            {//进行攻击

            }
            else
            {//走向敌人
                attackState = AttackState.Moving;
                playerMove.SimpleMove(targetTransform.position);
            }
        }
    }
}
