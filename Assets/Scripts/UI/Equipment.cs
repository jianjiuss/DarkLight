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
            equipmentItem.SetInfo(info);
        }

        return true;
    }
}
