using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillItem : MonoBehaviour 
{
    public int id;
    private UISprite icon;
    private UILabel name;
    private UILabel type;
    private UILabel des;
    private UILabel mp;
    private GameObject mask;

    private void InitProperty()
    {
        icon = transform.Find("Icon").GetComponentInChildren<UISprite>();
        name = transform.Find("Property/NameBg/Name").GetComponentInChildren<UILabel>();
        type = transform.Find("Property/TypeBg/Type").GetComponentInChildren<UILabel>();
        des = transform.Find("Property/DesBg/Des").GetComponentInChildren<UILabel>();
        mp = transform.Find("Property/MpBg/Mp").GetComponentInChildren<UILabel>();
        mask = transform.Find("Mask").gameObject;
        mask.SetActive(false);
    }

    public void UpdateShow(int currentLevel)
    {
        if(currentLevel >= SkillsInfo._instance.GetSkillInfoById(id).level)
        {
            mask.SetActive(false);
            GetComponentInChildren<SkillIcon>().enabled = true;
        }
        else
        {
            mask.SetActive(true);
            GetComponentInChildren<SkillIcon>().enabled = false;
        }
    }

    public void SetId(int id)
    {
        InitProperty();
        SkillInfo info = SkillsInfo._instance.GetSkillInfoById(id);
        icon.spriteName = info.icon_name;
        name.text = info.name;
        switch(info.applyType)
        {
            case ApplyType.Buff:
                type.text = "增强";
                break;
            case ApplyType.Passive:
                type.text = "增益";
                break;
            case ApplyType.SingleTarget:
                type.text = "单个目标";
                break;
            case ApplyType.MultiTarget:
                type.text = "群体目标";
                break;
        }
        des.text = info.des;
        mp.text = info.mp + "MP";
        this.id = info.id;
    }
}
