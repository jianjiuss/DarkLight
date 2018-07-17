using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarNPC : MonoBehaviour 
{
    public static BarNPC _Instance;

    public TweenPosition questTweenPosition;
    public GameObject okButtonGo;
    public GameObject acceptButtonGo;
    public GameObject cancelButtonGo;
    public UILabel DesLabel;
    public int killCount = 0;

    private bool isInTask;
    private PlayerStatus playerStatus;

    void Awake()
    {
        _Instance = this;
    }

    void Start()
    {
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShowQuest();
        }
    }

    void ShowQuest()
    {
        questTweenPosition.gameObject.SetActive(true);
        if (isInTask)
        {
            ShowTaskQuest();
        }
        else
        {
            ShowDesQuest();
        }
    }

    void HideQuest()
    {
        questTweenPosition.PlayReverse();
    }

    public void OnOkButtonClick()
    {
        if (killCount >= 10)
        {
            playerStatus.GetCoin(1000);
            killCount = 0;
            isInTask = false;
            ShowDesQuest();
        }
        else
        {
            HideQuest();
        }
    }

    public void OnCloseButtonClick()
    {
        HideQuest();
    }

    public void OnCancelButtonClick()
    {
        HideQuest();
    }

    public void OnAcceptButtonClick()
    {
        isInTask = true;
        ShowTaskQuest();
    }

    void ShowTaskQuest()
    {
        questTweenPosition.PlayForward();
        DesLabel.text = "任务：\n你已经杀死了" + killCount + "/10只狼\n\n奖励：\n1000金币";
        okButtonGo.SetActive(true);
        acceptButtonGo.SetActive(false);
        cancelButtonGo.SetActive(false);
    }

    void ShowDesQuest()
    {
        questTweenPosition.PlayForward();
        DesLabel.text = "任务：\n杀死10只小野狼\n\n奖励：\n1000金币";
        okButtonGo.SetActive(false);
        acceptButtonGo.SetActive(true);
        cancelButtonGo.SetActive(true);
    }

    public void OnKillWolf()
    {
        if(isInTask)
        {
            killCount++;
        }
    }
}
