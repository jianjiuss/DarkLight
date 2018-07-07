using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemGrid : MonoBehaviour 
{
    public int id = 0;
    public int currentNumb = 0;
    private UILabel numLabel;
    private ObjectInfo info;

	void Start () 
    {
        numLabel = this.GetComponentInChildren<UILabel>();
	}

    public void PlusNum(int numb = 1)
    {
        this.currentNumb += numb;
        numLabel.text = this.currentNumb.ToString();
    }

    public bool SubNum(int numb = 1)
    {
        if(currentNumb < numb)
        {
            return false;
        }

        currentNumb -= numb;
        if(currentNumb == 0)
        {
            ClearInfo();
            GameObject item = GetComponentInChildren<InventoryItem>().gameObject;
            Destroy(item);
        }
        else
        {
            numLabel.text = this.currentNumb.ToString();
        }

        return true;
    }

    public void SetId(int id, int num = 1)
    {
        info = ObjectsInfo._Instance.GetObjectInfo(id);
        InventoryItem item = GetComponentInChildren<InventoryItem>();
        item.SetIconName(id, info.icon_name);
        numLabel.enabled = true;
        this.currentNumb = num;
        numLabel.text = num.ToString();
        this.id = id;
    }

    public void ClearInfo()
    {
        id = 0;
        info = null;
        currentNumb = 0;
        numLabel.enabled = false;
    }
}
