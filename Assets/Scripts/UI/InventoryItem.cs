﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : UIDragDropItem 
{
    private UISprite sprite;
    private int itemId;
    private bool isHover = false;
    private PlayerStatus playerStatus;

    void Awake()
    {
        sprite = this.GetComponent<UISprite>();
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
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
            UseItem();
        }
    }

    public bool UseItem()
    {
        ItemDes._Instance.Hide(itemId);
        ObjectInfo info = ObjectsInfo._Instance.GetObjectInfo(itemId);
        if(info.type == ObjectType.Equip)
        {
            bool dressSuccess = Equipment._Instance.Dress(itemId);
            if (dressSuccess)
            {
                InventoryItemGrid itemGrid = gameObject.transform.parent.GetComponent<InventoryItemGrid>();
                itemGrid.SubNum();
            }
        }
        else if(info.type == ObjectType.Drug)
        {
            InventoryItemGrid itemGrid = gameObject.transform.parent.GetComponent<InventoryItemGrid>();
            if(itemGrid.SubNum())
            {
                playerStatus.PlusHpAndMp(info.hp, info.mp);
                return true;
            }
        }
        return false;
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
                    newItemGrid.SetId(oldItemGrid.id, oldItemGrid.currentNumb);
                    oldItemGrid.ClearInfo();
                }
            }
            else if(surface.tag == Tags.inventory_item)
            {
                InventoryItemGrid oldItemGrid = gameObject.transform.parent.GetComponent<InventoryItemGrid>();
                InventoryItemGrid newItemGrid = surface.transform.parent.GetComponent<InventoryItemGrid>();

                int tempId = newItemGrid.id;
                int tempNum = newItemGrid.currentNumb;
                
                newItemGrid.ClearInfo();
                this.transform.parent = newItemGrid.gameObject.transform;
                newItemGrid.SetId(oldItemGrid.id, oldItemGrid.currentNumb);
                
                oldItemGrid.ClearInfo();
                InventoryItem newItem = newItemGrid.GetComponentInChildren<InventoryItem>();
                newItem.gameObject.transform.parent = oldItemGrid.gameObject.transform;
                oldItemGrid.SetId(tempId, tempNum);
                newItem.ResetPosition();
            }
            else if(surface.tag == Tags.shortcut)
            {
                ShortcutGrid shortcutGrid = surface.GetComponent<ShortcutGrid>();
                shortcutGrid.SetInventory(itemId);
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
