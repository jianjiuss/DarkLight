using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WolfState
{
    Idle,
    Walk,
    Attack,
    Death
}

public class WolfBaby : MonoBehaviour 
{
    public WolfState state = WolfState.Idle;

    public string deathAnimationName;
    public string idleAnimationName;
    public string normalAttackAnimationName;
    public string crazyAttackAnimationName;
    public string walkAnimationName;
    public string takeDamageAnimationName;
    public float timer = 1;
    public int speed = 2;
    public float dodgeProbability = 0.2f;
    public int hp = 100;
    public int attack = 5;
    public AudioClip missAudio;

    public GameObject hudPrefab;
    private GameObject hudTextGo;
    private GameObject hudTextFollow;
    private HUDText hudText;
    private UIFollowTarget follow;

    public float time_normalattack;
    public float time_crazyattack;
    public float crazyattack_rate;
    public int attack_rate = 1;//攻击速率 每秒
    public string aniname_attack_now;
    private float attack_timer = 0;

    public Transform target;
    public float minDistance = 2;
    public float maxDistance = 5;
    
    private new Animation animation;
    private CharacterController controller;
    private float currentTime;
    private new Renderer renderer;

    void Awake()
    {
        animation = GetComponent<Animation>();
        controller = GetComponent<CharacterController>();
        renderer = transform.Find("Wolf_Baby").GetComponent<Renderer>();
        hudTextFollow = transform.Find("HUDText").gameObject;
    }

    void Start()
    {
        hudTextGo = NGUITools.AddChild(HudTextParent._Instance.gameObject, hudPrefab);
        hudText = hudTextGo.GetComponent<HUDText>();
        follow = hudTextGo.GetComponent<UIFollowTarget>();
        follow.target = hudTextFollow.transform;
        follow.gameCamera = Camera.main;
    }

    void Update()
    {
        if(state == WolfState.Death)
        {
            animation.CrossFade(deathAnimationName);
            Destroy(gameObject, 1);
        }
        else if(state == WolfState.Attack)
        {
            AutoAttack();
        }
        else if(state == WolfState.Idle)
        {
            animation.CrossFade(idleAnimationName);
        }
        else if(state == WolfState.Walk)
        {
            animation.CrossFade(walkAnimationName);
            controller.SimpleMove(transform.forward * speed);
        }

        if (state != WolfState.Death && state != WolfState.Attack)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= timer)
            {
                currentTime = 0;
                WolfState nextState = RandomState();
                state = nextState;
            }
        }
    }

    private WolfState RandomState()
    {
        int value = Random.Range(0, 2);
        if (value == 0)
        {
            return WolfState.Idle;
        }
        else
        {
            if (state == WolfState.Idle)
            {
                transform.Rotate(new Vector3(0, Random.Range(0, 360), 0));
            }
            return WolfState.Walk;
        }
    }

    private void AutoAttack()
    {
        if (target != null)
        {
            //PlayerState playerState = target.GetComponent<PlayerAttack>().state;
            //if (playerState == PlayerState.Death)
            //{
            //    target = null;
            //    state = WolfState.Idle; return;
            //}
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance > maxDistance)
            {//停止自动攻击
                target = null;
                state = WolfState.Idle;
            }
            else if (distance <= minDistance)
            {//自动攻击
                attack_timer += Time.deltaTime;
                animation.CrossFade(aniname_attack_now);
                if (aniname_attack_now == normalAttackAnimationName)
                {
                    if (attack_timer > time_normalattack)
                    {
                        //产生伤害 
                        //target.GetComponent<PlayerAttack>().TakeDamage(attack);
                        aniname_attack_now = idleAnimationName;
                    }
                }
                else if (aniname_attack_now == crazyAttackAnimationName)
                {
                    if (attack_timer > time_crazyattack)
                    {
                        //产生伤害 
                        //target.GetComponent<PlayerAttack>().TakeDamage(attack);
                        aniname_attack_now = idleAnimationName;
                    }
                }
                if (attack_timer > (1f / attack_rate))
                {
                    RandomAttack();
                    attack_timer = 0;
                }
            }
            else
            {//朝着角色移动
                transform.LookAt(target);
                controller.SimpleMove(transform.forward * speed);
                animation.CrossFade(walkAnimationName);
            }
        }
        else
        {
            state = WolfState.Idle;
        }
    }

    void RandomAttack()
    {
        float value = Random.Range(0f, 1f);
        if (value < crazyattack_rate)
        {//进行疯狂攻击
            aniname_attack_now = crazyAttackAnimationName;
        }
        else
        {//进行普通攻击
            aniname_attack_now = normalAttackAnimationName;
        }
    }

    public void TakeDamage(int damageValue)
    {
        if(state == WolfState.Death)
        {
            return;
        }
        
        float value = Random.Range(0f, 1f);
        target = GameObject.FindGameObjectWithTag(Tags.player).transform;
        state = WolfState.Attack;
        if(value <= dodgeProbability)
        {
            AudioSource.PlayClipAtPoint(missAudio, transform.position);
            hudText.Add("Miss", Color.gray, 1);
            return;
        }

        StartCoroutine(ChangeBodyRed());
        hp -= damageValue;
        if(hp <= 0)
        {
            state = WolfState.Death;
        }
        else
        {
            animation.Play(takeDamageAnimationName);
        }
    }

    IEnumerator ChangeBodyRed()
    {
        Color defalutColor = renderer.material.color;
        renderer.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        renderer.material.color = defalutColor;
    }

    IEnumerator ChangedState(WolfState newState, float time)
    {
        yield return new WaitForSeconds(time);
        state = newState;
    }

    void OnGUI()
    {
        if(GUILayout.Button("Attack"))
        {
            TakeDamage(10);
        }
    }

    void OnDestroy()
    {
        Destroy(hudTextGo);
    }
}
