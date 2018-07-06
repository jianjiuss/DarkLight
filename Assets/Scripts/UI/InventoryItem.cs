using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : UIDragDropItem 
{
    private UISprite sprite;
    private int itemId;
    private bool isHover = false;

    void Awake()
    {
        sprite = this.GetComponent<UISprite>();
    }

    protected override void Start()
    {
        base.Start();
        sprite.depth = 5;
    }

    protected override void Update()
    {
        base.Update();
        if(isHover && Input.GetMouseButtonDown(1))
        {
            Equipment._Instance.Dress(itemId);
        }
    }

    protected override void OnDragStart()
    {
        base.OnDragStart();
        ItemDes._Instance.Hide(itemId);
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
        ItemDes._Instance.Show(itemId);
    }

    public void OnHoverOut()
    {
        isHover = false;
        ItemDes._Instance.Hide(itemId);
    }

    
}
