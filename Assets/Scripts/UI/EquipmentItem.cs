using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : MonoBehaviour 
{
    private UISprite iconSprite;

    void Awake()
    {
        iconSprite = GetComponent<UISprite>();
    }

    public void SetId(int id)
    {
        ObjectInfo info = ObjectsInfo._Instance.GetObjectInfo(id);
        SetInfo(info);
    }

    public void SetInfo(ObjectInfo info)
    {
        iconSprite.spriteName = info.icon_name;
    }
}
