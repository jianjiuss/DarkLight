using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WolfState
{
    Idle,
    Walk,
    Attack,
    Death,
    TakeDamage
}

public class WolfBaby : MonoBehaviour 
{
    public WolfState state = WolfState.Idle;

    public string deathAnimationName;
    public string idleAnimationName;
    public string attackAnimationName;
    public string walkAnimationName;
    public string takeDamageAnimationName;
    public float timer = 1;
    public int speed = 2;
    public float dodgeProbability = 0.2f;
    public int hp = 100;
    
    
    private new Animation animation;
    private CharacterController controller;
    private float currentTime;
    private new Renderer renderer;

    void Awake()
    {
        animation = GetComponent<Animation>();
        controller = GetComponent<CharacterController>();
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if(state == WolfState.Death)
        {
            animation.CrossFade(deathAnimationName);
            if(!animation.IsPlaying(deathAnimationName))
            {
                GameObject.DestroyImmediate(gameObject);
            }
        }
        else if(state == WolfState.Attack)
        {
            //TODO
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
        else if(state == WolfState.TakeDamage)
        {
            animation.CrossFade(takeDamageAnimationName);
            if(!animation.IsPlaying(takeDamageAnimationName))
            {
                state = WolfState.Idle;
            }
        }

        if(state != WolfState.Death || state != WolfState.Attack)
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
        if(value == 0)
        {
            return WolfState.Idle;
        }
        else
        {
            if(state == WolfState.Idle)
            {
                transform.Rotate(new Vector3(0, Random.Range(0, 360), 0));
            }
            return WolfState.Walk;
        }
    }

    public void TakeDamage(int damageValue)
    {
        StartCoroutine(ChangeBodyRed());
        hp -= damageValue;
        if(hp <= 0)
        {
            state = WolfState.Death;
        }
    }

    IEnumerator ChangeBodyRed()
    {
        Color defalutColor = renderer.material.color;
        renderer.material.color = Color.red;
        yield return new WaitForSeconds(1);
        renderer.material.color = defalutColor;
    }

    void OnGUI()
    {
        if(GUILayout.Button("Attack"))
        {
            TakeDamage(1);
        }
    }
}
