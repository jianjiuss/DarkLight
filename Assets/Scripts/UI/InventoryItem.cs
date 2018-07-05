using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : UIDragDropItem 
{
    private UISprite sprite;
    private bool isHover = false;
    private int itemId;

    void Awake()
    {
        sprite = this.GetComponent<UISprite>();
    }

    protected override void Start()
    {
        base.Start();
        sprite.depth = 5;
    }

    bool isShowDes = false;
    protected override void Update()
    {
        base.Update();
        if (isHover && !isShowDes)
        {
            isShowDes = true;
            ItemDes._Instance.Show(itemId);
        }

        if (!isHover && isShowDes)
        {
            isShowDes = false;
            ItemDes._Instance.Hide();
        }
            
    }

    protected override void OnDragStart()
    {
        base.OnDragStart();
        isHover = false;
    }

    protected override void OnDragDropRelease(GameObject surface)
    {
        base.OnDragDropRelease(surface);
        if (surface != null)
        {
            if (surface.tag == Tags.inventory_item_grid)
            {
                InventoryItemGrid oldItemGrid = gameObject.transform.parent.GetComponent<InventoryItemGrid>();
                InventoryItemGrid newItemGrid = surface.GetComponent<InventoryItemGrid>();
                if (newItemGrid != oldItemGrid)
                {
                    this.transform.parent = surface.transform;
                    newItemGrid.SetId(oldItemGrid.id, oldItemGrid.num);
                    oldItemGrid.ClearInfo();
                }
            }

            else if(surface.tag == Tags.inventory_item)
            {
                InventoryItemGrid oldItemGrid = gameObject.transform.parent.GetComponent<InventoryItemGrid>();
                InventoryItemGrid newItemGrid = surface.transform.parent.GetComponent<InventoryItemGrid>();

                int tempId = newItemGrid.id;
                int tempNum = newItemGrid.num;
                
                newItemGrid.ClearInfo();
                this.transform.parent = newItemGrid.gameObject.transform;
                newItemGrid.SetId(oldItemGrid.id, oldItemGrid.num);
                
                oldItemGrid.ClearInfo();
                InventoryItem newItem = newItemGrid.GetComponentInChildren<InventoryItem>();
                newItem.gameObject.transform.parent = oldItemGrid.gameObject.transform;
                oldItemGrid.SetId(tempId, tempNum);
                newItem.ResetPosition();
            }
        }
        ResetPosition();
    }

    private void ResetPosition()
    {
        this.gameObject.transform.localPosition = Vector3.zero;
    }

    public void SetId(int id)
    {
        ObjectInfo info = ObjectsInfo._Instance.GetObjectInfo(id);
        sprite.name = info.icon_name;
    }

    public void SetIconName(int id,string iconName)
    {
        sprite.name = iconName;
        sprite.spriteName = iconName;
        itemId = id;
    }

    public void OnHoverOver()
    {
        isHover = true;
    }

    public void OnHoverOut()
    {
        isHover = false;
    }
}
