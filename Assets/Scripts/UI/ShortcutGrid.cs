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
    private UISprite shortcutIcon;
    private SkillInfo skillInfo;
    private ObjectInfo objectInfo;
    private GridType gridType;

    private PlayerStatus ps;
    private PlayerAttack pa;


    void Awake()
    {
        shortcutIcon = transform.Find("Icon").GetComponent<UISprite>();
        shortcutIcon.gameObject.SetActive(false);
    }

    void Start()
    {
        ps = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        pa = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerAttack>();
    }

    void Update()
    {
        if(Input.GetKeyDown(keycode))
        {
            UseItem();
        }
    }

    private void UseItem()
    {
        if(gridType == GridType.Inventory)
        {
            if(!Inventory._Instance.UseItem(objectInfo.id))
            {
                shortcutIcon.gameObject.SetActive(false);
                objectInfo = null;
                gridType = GridType.None;
            }
        }
        else if(gridType == GridType.Skill)
        {
            bool success = ps.TakeMP(skillInfo.mp);
            if(!success)
            {
                return;
            }
            pa.UseSkill(skillInfo);
        }
    }

    public void SetSkill(int id)
    {
        objectInfo = null;
        shortcutIcon.gameObject.SetActive(true);
        gridType = GridType.Skill;
        skillInfo = SkillsInfo._instance.GetSkillInfoById(id);
        shortcutIcon.spriteName = skillInfo.icon_name;
    }

    public void SetInventory(int id)
    {
        skillInfo = null;
        ObjectInfo info = ObjectsInfo._Instance.GetObjectInfo(id);
        if(info.type != ObjectType.Drug)
        {
            return;
        }

        shortcutIcon.gameObject.SetActive(true);
        gridType = GridType.Inventory;
        objectInfo = info;
        shortcutIcon.spriteName = info.icon_name;
    }
}
