using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugShop : MonoBehaviour 
{
    public static DrugShop _Instance;

    private bool isShow = false;
    private TweenPosition tweenPosition;
    void Awake()
    {
        _Instance = this;
        tweenPosition = this.GetComponent<TweenPosition>();
    }

    public void OnBuyItem1001Click()
    { 

    }

    public void OnBuyItem1002Click()
    {

    }

    public void OnBuyItem1003Click()
    { 

    }

    public void SwitchDrugShop()
    {
        if(isShow)
        {
            isShow = false;
            tweenPosition.PlayReverse();
        }
        else
        {
            isShow = true;
            tweenPosition.PlayForward();
        }
    }
}
