using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugShop : MonoBehaviour 
{
    public static DrugShop _Instance;

    private bool isShow = false;
    private TweenPosition tweenPosition;
    private GameObject confirmField;
    private UILabel buyNumbLabel;
    private int currentBuyId = 0;
    private PlayerStatus playerStatus;

    void Awake()
    {
        _Instance = this;
        tweenPosition = this.GetComponent<TweenPosition>();
        confirmField = transform.Find("ConfirmField").gameObject;
        confirmField.SetActive(false);
        buyNumbLabel = confirmField.GetComponentInChildren<UILabel>();
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
    }

    public void OnBuyItem1001Click()
    {
        confirmField.SetActive(true);
        currentBuyId = 1001;
    }

    public void OnBuyItem1002Click()
    {
        confirmField.SetActive(true);
        currentBuyId = 1002;
    }

    public void OnBuyItem1003Click()
    {
        confirmField.SetActive(true);
        currentBuyId = 1003;
    }

    public void OnOkButtonClick()
    {
        int buyNumb = int.Parse(buyNumbLabel.text);
        if(buyNumb <= 0)
        {
            return;
        }
        ObjectInfo buyInfo = ObjectsInfo._Instance.GetObjectInfo(currentBuyId);
        int spendMoney = buyInfo.price_buy * buyNumb;
        if(!playerStatus.GetMoney(spendMoney))
        {
            return;
        }
        Inventory._Instance.GetItem(buyInfo.id, buyNumb);
        buyNumb = 0;
        confirmField.SetActive(false);
    }

    public void SwitchDrugShop()
    {
        if(isShow)
        {
            isShow = false;
            tweenPosition.PlayReverse();
            confirmField.SetActive(false);
        }
        else
        {
            isShow = true;
            tweenPosition.PlayForward();
        }
    }
}
