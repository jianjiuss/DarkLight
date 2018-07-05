using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDes : MonoBehaviour 
{
    public static ItemDes _Instance;
    private UILabel desLabel;

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
        }
        desLabel.text = des;
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);
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
}
