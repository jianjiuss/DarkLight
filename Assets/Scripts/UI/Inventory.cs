using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour 
{
    public static Inventory _Instance;

    private TweenPosition tween;

    public List<InventoryItemGrid> itemGridList = new List<InventoryItemGrid>();
    public UILabel coinNumberLabel;
    public GameObject inventoryItem;
    public bool isInventoryShow = false;

    private PlayerStatus playerStatus;

    void Awake()
    {
        _Instance = this;
        tween = this.GetComponent<TweenPosition>();
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        UpdateCoinLabel();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GetItem(Random.Range(2001, 2022));
        }
    }

    public void UpdateCoinLabel()
    {
        coinNumberLabel.text = playerStatus.coin.ToString();
    }

    public void GetItem(int id, int numb = 1)
    {
        bool isExist = false;
        foreach (var item in itemGridList)
        {
            if (item.id == id)
            {
                isExist = true;
                item.PlusNum(numb);
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
                item.SetId(id,numb);
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

    public bool UseItem(int id)
    {
        foreach(var item in itemGridList)
        {
            if(item.id == id)
            {
                return item.GetComponentInChildren<InventoryItem>().UseItem();
            }
        }
        return false;
    }
}
