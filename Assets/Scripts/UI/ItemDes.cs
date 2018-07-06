using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDes : MonoBehaviour 
{
    public static ItemDes _Instance;
    private UILabel desLabel;
    private int currentDesItemId;

    void Awake()
    {
        _Instance = this;
        this.gameObject.SetActive(false);
        desLabel = transform.Find("DesLabel").gameObject.GetComponent<UILabel>();
    }

    public void Show(int itemId)
    {
        this.gameObject.SetActive(true);
        transform.position = UICamera.currentCamera.ScreenToWorldPoint(Input.mousePosition);
        ObjectInfo info = ObjectsInfo._Instance.GetObjectInfo(itemId);
        string des = string.Empty;
        switch (info.type)
        {
            case ObjectType.Drug:
                des = GetDrugDes(info);
                break;
            case ObjectType.Equip:
                des = GetEquipDes(info);
                break;
        }
        desLabel.text = des;
        currentDesItemId = itemId;
    }

    public void Hide(int itemId)
    {
        if(itemId == currentDesItemId)
        {
            this.gameObject.SetActive(false);
        }
    }

    string GetDrugDes(ObjectInfo info)
    {
        string str = "";
        str += "名称：" + info.name + "\n";
        str += "+HP : " + info.hp + "\n";
        str += "+MP：" + info.mp + "\n";
        str += "出售价：" + info.price_sell + "\n";
        str += "购买价：" + info.price_buy;

        return str;
    }

    string GetEquipDes(ObjectInfo info)
    {
        string str = "";
        str += "名称：" + info.name + "\n";
        switch (info.dressType)
        {
            case DressType.Headgear:
                str += "穿戴类型：头盔\n";
                break;
            case DressType.Armor:
                str += "穿戴类型：盔甲\n";
                break;
            case DressType.LeftHand:
                str += "穿戴类型：左手\n";
                break;
            case DressType.RightHand:
                str += "穿戴类型：右手\n";
                break;
            case DressType.Shoe:
                str += "穿戴类型：鞋\n";
                break;
            case DressType.Accessory:
                str += "穿戴类型：饰品\n";
                break;
        }
        switch (info.applicationType)
        {
            case ApplicationType.Swordman:
                str += "适用类型：剑士\n";
                break;
            case ApplicationType.Magician:
                str += "适用类型：魔法师\n";
                break;
            case ApplicationType.Common:
                str += "适用类型：通用\n";
                break;
        }

        str += "伤害值：" + info.attack + "\n";
        str += "防御值：" + info.defend + "\n";
        str += "速度值：" + info.speed + "\n";

        str += "出售价：" + info.price_sell + "\n";
        str += "购买价：" + info.price_buy;

        return str;
    }
}
