using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour 
{
    public static Skill _Instance;
    public GameObject skillItemPrefab;
    private TweenPosition tween;
    private bool isShow = false;
    private PlayerStatus playerStatus;

    private int[] ids;

    void Awake()
    {
        _Instance = this;
        tween = GetComponent<TweenPosition>();
    }

    void Start()
    {
        GameObject grid = transform.Find("Scroll View/Grid").gameObject;
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        switch(playerStatus.heroType)
        {
            case HeroType.Magician:
                ids = SkillsInfo._instance.GetSkillInfoBy(ApplicableRole.Magician);
                break;
            case HeroType.Swordman:
                ids = SkillsInfo._instance.GetSkillInfoBy(ApplicableRole.Swordman);
                break;
        }
        UIGrid uigrid = grid.GetComponent<UIGrid>();
        foreach(int id in ids)
        {
            GameObject go = NGUITools.AddChild(grid, skillItemPrefab);
            uigrid.AddChild(go.transform);
            SkillItem item = go.GetComponent<SkillItem>();
            item.SetId(id);
        }
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
            UpdateShow();
        }
    }

    private void UpdateShow()
    {
        SkillItem[] skillItems = GetComponentsInChildren<SkillItem>();
        foreach(var item in skillItems)
        {
            item.UpdateShow(playerStatus.level);
        }
    }
}
