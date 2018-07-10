using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : MonoBehaviour 
{
    private ObjectInfo info;

    private UISprite icon;
    private UILabel itemName;
    private UILabel effect;
    private UILabel buyPrice;

    private PlayerStatus playerStatus;

    void Awake()
    {
        itemName = transform.Find("ItemName").GetComponent<UILabel>();
        icon = transform.Find("Icon").GetComponent<UISprite>();
        effect = transform.Find("Effect").GetComponent<UILabel>();
        buyPrice = transform.Find("BuyPrice").GetComponent<UILabel>();

        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
    }

    public void SetId(int id)
    {
        this.info = ObjectsInfo._Instance.GetObjectInfo(id);

        itemName.text = info.name;
        icon.spriteName = info.icon_name;
        if(info.attack > 0)
        {
            effect.text = "伤害：+" + info.attack;
        }
        else if(info.defend > 0)
        {
            effect.text = "防御：+" + info.defend;
        }
        else if(info.speed > 0)
        {
            effect.text = "速度：+" + info.speed;
        }
        buyPrice.text = info.price_buy.ToString();
    }

    public void OnBuyButtonClick()
    {
        if(playerStatus.GetMoney(info.price_buy))
        {
            Inventory._Instance.GetItem(info.id);
        }
    }
}
