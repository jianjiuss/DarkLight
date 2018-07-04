using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour 
{
    public static Inventory _Instance;

    private TweenPosition tween;
    private int coinCount = 1000;

    public List<InventoryItemGrid> itemGridList = new List<InventoryItemGrid>();
    public UILabel coinNumberLabel;
    public GameObject inventoryItem;
    public bool isInventoryShow = false;

    void Awake()
    {
        _Instance = this;
        tween = this.GetComponent<TweenPosition>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GetItem(Random.Range(1001, 1004));
        }
    }

    public void GetItem(int id)
    {
        bool isExist = false;
        foreach (var item in itemGridList)
        {
            if (item.id == id)
            {
                isExist = true;
                item.PlusNum();
            }
        }

        if (isExist)
        {
            return;
        }

        foreach (var item in itemGridList)
        {
            if (item.id == 0)
            {
                GameObject itemObject = NGUITools.AddChild(item.gameObject, inventoryItem);
                itemObject.transform.localPosition = Vector3.zero;
                item.SetId(id);
                return;
            }
        }
    }

    public void Show()
    {
        isInventoryShow = true;
        tween.PlayForward();
    }

    public void Hide()
    {
        isInventoryShow = false;
        tween.PlayReverse();
    }
}
