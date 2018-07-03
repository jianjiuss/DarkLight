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

    void Awake()
    {
        _Instance = this;
        tween = this.GetComponent<TweenPosition>();
    }

    public void Show()
    {
        tween.PlayForward();
    }

    public void Hide()
    {
        tween.PlayReverse();
    }
}
