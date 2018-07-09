using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillIcon : UIDragDropItem 
{
    private int skillId;
    protected override void OnDragDropStart()
    {
        base.OnDragDropStart();
        skillId = transform.parent.GetComponent<SkillItem>().id;
        transform.parent = transform.root;
        this.GetComponent<UISprite>().depth = 40;
    }

    protected override void OnDragDropRelease(GameObject surface)
    {
        base.OnDragDropRelease(surface);
        if(surface != null && surface.tag == Tags.shortcut)
        {
            surface.GetComponent<ShortcutGrid>().SetSkill(skillId);
        }
    }
}
