using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour 
{
    public static Skill _Instance;
    private TweenPosition tween;
    private bool isShow = false;

    void Awake()
    {
        _Instance = this;
        tween = GetComponent<TweenPosition>();
    }

    public void SwitchSkill()
    {
        if(isShow)
        {
            isShow = false;
            tween.PlayReverse();
        }
        else
        {
            isShow = true;
            tween.PlayForward();
        }
    }
}
