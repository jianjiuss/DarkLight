using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : UIDragDropItem 
{
    private UISprite sprite;

    void Awake()
    {
        sprite = this.GetComponent<UISprite>();
    }

    protected override void Start()
    {
        base.Start();
        sprite.depth = 4;
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
                InventoryItemGrid newItemGrid = surface.GetComponent<InventoryItemGrid>();

                int tempId = newItemGrid.id;
                int tempNum = newItemGrid.num;
                
                newItemGrid.ClearInfo();
                newItemGrid.SetId(oldItemGrid.id, oldItemGrid.num);
                this.transform.parent = surface.transform;
                
                oldItemGrid.ClearInfo();
                oldItemGrid.SetId(tempId, tempNum);
                InventoryItem newItem = newItemGrid.GetComponentInChildren<InventoryItem>();
                newItem.gameObject.transform.parent = oldItemGrid.gameObject.transform;
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

    public void SetIconName(string iconName)
    {
        sprite.name = iconName;
        sprite.spriteName = iconName;
    }
}
