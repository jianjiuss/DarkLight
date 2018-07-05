using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour 
{
    public static Status _Instance;

    public TweenPosition tweenPosition;

    private UILabel attackCount;
    private UILabel defendCount;
    private UILabel speedCount;
    private UILabel pointCount;
    private UILabel summary;

    private GameObject addAttackButton;
    private GameObject addDefendButton;
    private GameObject addSpeedButton;

    private PlayerStatus playerStatus;
    private bool isShow = false;

    void Awake()
    {
        _Instance = this;

        attackCount = transform.Find("AttackCount").GetComponent<UILabel>();
        defendCount = transform.Find("DefendCount").GetComponent<UILabel>();
        speedCount = transform.Find("SpeedCount").GetComponent<UILabel>();
        pointCount = transform.Find("PointCount").GetComponent<UILabel>();
        summary = transform.Find("Summary").GetComponent<UILabel>();

        addAttackButton = transform.Find("AddAttackButton").gameObject;
        addDefendButton = transform.Find("AddDefendButton").gameObject;
        addSpeedButton = transform.Find("AddSpeedButton").gameObject;

        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();

        tweenPosition = GetComponent<TweenPosition>();

        gameObject.SetActive(false);
    }

    public void UpdateShow()
    {
        attackCount.text = playerStatus.attack + " + " + playerStatus.attack_plus;
        defendCount.text = playerStatus.def + " + " + playerStatus.def_plus;
        speedCount.text = playerStatus.speed + " + " + playerStatus.speed_plus;

        pointCount.text = playerStatus.poin_remain.ToString();

        summary.text = "伤害：" + (playerStatus.attack + playerStatus.attack_plus)
            + "  " + "防御：" + (playerStatus.def + playerStatus.def_plus)
            + "  " + "速度：" + (playerStatus.speed + playerStatus.speed_plus);

        if (playerStatus.poin_remain > 0)
        {
            addAttackButton.SetActive(true);
            addDefendButton.SetActive(true);
            addSpeedButton.SetActive(true);
        }
        else
        {
            addAttackButton.SetActive(false);
            addDefendButton.SetActive(false);
            addSpeedButton.SetActive(false);
        }
    }

    public void OnAddAttackButtonClick()
    {
        bool havePoint = playerStatus.GetPoint();
        if(havePoint)
        {
            playerStatus.attack_plus++;
        }
        UpdateShow();
    }

    public void OnAddDefendButtonClick()
    {
        bool havePoint = playerStatus.GetPoint();
        if(havePoint)
        {
            playerStatus.def_plus++;
        }
        UpdateShow();
    }

    public void OnAddSpeedButtonClick()
    {
        bool havePoint = playerStatus.GetPoint();
        if (havePoint)
        {
            playerStatus.speed_plus++;
        }
        UpdateShow();
    }

    public void SwitchStatus()
    {
        if (isShow)
        {
            isShow = false;
            tweenPosition.PlayReverse();
        }
        else
        {
            if (!gameObject.activeInHierarchy)
            {
                gameObject.SetActive(true);
            }
            isShow = true;
            tweenPosition.PlayForward();
            UpdateShow();
        }
    }
}
