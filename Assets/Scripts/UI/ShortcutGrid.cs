using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridType
{
    Skill,
    Inventory,
    None
}

public class ShortcutGrid : MonoBehaviour 
{
    public KeyCode keycode;
    private UISprite skillIcon;
    private SkillInfo skillInfo;
    private GridType gridType;

    void Awake()
    {
        skillIcon = transform.Find("Icon").GetComponent<UISprite>();
        skillIcon.gameObject.SetActive(false);
    }

    public void SetSkill(int id)
    {
        skillIcon.gameObject.SetActive(true);
        gridType = GridType.Skill;
        skillInfo = SkillsInfo._instance.GetSkillInfoById(id);
        skillIcon.spriteName = skillInfo.icon_name;
    }

    public void SetInventory(int id)
    {

    }
}
