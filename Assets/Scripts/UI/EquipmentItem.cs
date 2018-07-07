using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentItem : MonoBehaviour 
{
    private bool isHover = false;
    private UISprite iconSprite;
    public int currentItemId;

    void Awake()
    {
        iconSprite = GetComponent<UISprite>();
    }

    void Update()
    {
        if(isHover)
        {
            if(Input.GetMouseButtonDown(1))
            {
                Equipment._Instance.TakeOff(currentItemId, gameObject);
            }
        }
    }

    public void SetId(int id)
    {
        ObjectInfo info = ObjectsInfo._Instance.GetObjectInfo(id);
        SetInfo(info);
    }

    public void SetInfo(ObjectInfo info)
    {
        currentItemId = info.id;
        iconSprite.spriteName = info.icon_name;
    }

    public void OnHover(bool isHover)
    {
        this.isHover = isHover;
    }

}
