using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour 
{
    public static Equipment _Instance;

    public GameObject equipmentItemPrefabs;

    private TweenPosition tween;
    private bool isShow;

    private GameObject headgear;
    private GameObject armor;
    private GameObject leftHand;
    private GameObject rightHand;
    private GameObject shoe;
    private GameObject accessory;
    private PlayerStatus playerStatus;

    private int attack;
    private int defend;
    private  int speed;

    void Awake()
    {
        _Instance = this;
        tween = GetComponent<TweenPosition>();

        headgear = transform.Find("Headgear").gameObject;
        armor = transform.Find("Armor").gameObject;
        leftHand = transform.Find("Left-Hand").gameObject;
        rightHand = transform.Find("Right-Hand").gameObject;
        shoe = transform.Find("Shoe").gameObject;
        accessory = transform.Find("Accessory").gameObject;
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
    }

    public void SwitchEquipment()
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

    public bool Dress(int id)
    {
        ObjectInfo info = ObjectsInfo._Instance.GetObjectInfo(id);
        if(!(info.type == ObjectType.Equip))
        {
            return false;
        }

        if(info.applicationType != ApplicationType.Common && !info.applicationType.ToString().Equals(playerStatus.heroType.ToString()))
        {
            return false;
        }

        GameObject parent = null;
        switch(info.dressType)
        {
            case DressType.Accessory:
                parent = accessory;
                break;
            case DressType.Armor:
                parent = armor;
                break;
            case DressType.Headgear:
                parent = headgear;
                break;
            case DressType.LeftHand:
                parent = leftHand;
                break;
            case DressType.RightHand:
                parent = rightHand;
                break;
            case DressType.Shoe:
                parent = shoe;
                break;
        }

        EquipmentItem equipmentItem = parent.GetComponentInChildren<EquipmentItem>();
        if(equipmentItem == null)
        {
            GameObject item = NGUITools.AddChild(parent, equipmentItemPrefabs);
            item.transform.localPosition = Vector3.zero;
            item.GetComponent<EquipmentItem>().SetInfo(info);
        }
        else
        {
            Inventory._Instance.GetItem(equipmentItem.currentItemId);
            equipmentItem.SetInfo(info);
        }
        UpdateProperty();
        return true;
    }

    public void TakeOff(int id, GameObject go)
    {
        Inventory._Instance.GetItem(id);
        DestroyImmediate(go);
        UpdateProperty();
    }

    void UpdateProperty()
    {
        attack = 0;
        defend = 0;
        speed = 0;

        EquipmentItem headgearItem = headgear.GetComponentInChildren<EquipmentItem>();
        PlusProperty(headgearItem);
        EquipmentItem armorItem = armor.GetComponentInChildren<EquipmentItem>();
        PlusProperty(armorItem);
        EquipmentItem leftHandItem = leftHand.GetComponentInChildren<EquipmentItem>();
        PlusProperty(leftHandItem);
        EquipmentItem rightHandItem = rightHand.GetComponentInChildren<EquipmentItem>();
        PlusProperty(rightHandItem);
        EquipmentItem shoeItem = shoe.GetComponentInChildren<EquipmentItem>();
        PlusProperty(shoeItem);
        EquipmentItem accessoryItem = accessory.GetComponentInChildren<EquipmentItem>();
        PlusProperty(accessoryItem);

        //Debug.Log("attack:" + attack + ",defend:" + defend + ",speed:" + speed);
    }

    void PlusProperty(EquipmentItem item)
    {
        if(item == null)
        {
            return;
        }

        PlusProperty(item.currentItemId);
    }

    void PlusProperty(int id)
    {
        ObjectInfo info = ObjectsInfo._Instance.GetObjectInfo(id);
        PlusProperty(info);
    }

    void PlusProperty(ObjectInfo info)
    {
        if(info == null)
        {
            return;
        }

        attack += info.attack;
        defend += info.defend;
        speed += info.speed;
    }
}
