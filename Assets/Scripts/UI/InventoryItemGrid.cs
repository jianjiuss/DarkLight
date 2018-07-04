using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemGrid : MonoBehaviour 
{
    public int id = 0;
    public int num = 0;
    private UILabel numLabel;
    private ObjectInfo info;

	void Start () 
    {
        numLabel = this.GetComponentInChildren<UILabel>();
	}

    public void PlusNum(int num = 1)
    {
        this.num += num;
        numLabel.text = this.num.ToString();
    }

    public void SetId(int id, int num = 1)
    {
        info = ObjectsInfo._Instance.GetObjectInfo(id);
        InventoryItem item = GetComponentInChildren<InventoryItem>();
        item.SetIconName(info.icon_name);
        numLabel.enabled = true;
        this.num = num;
        numLabel.text = num.ToString();
        this.id = id;
    }

    public void ClearInfo()
    {
        id = 0;
        info = null;
        num = 0;
        numLabel.enabled = false;
    }
}
