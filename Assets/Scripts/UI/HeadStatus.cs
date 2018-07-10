using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadStatus : MonoBehaviour
{
    public static HeadStatus _Instance;

    private UILabel headName;

    private UISlider hpBar;
    private UISlider mpBar;

    private UILabel hpLabel;
    private UILabel mpLabel;

    private PlayerStatus playerStatus;

    private void Awake()
    {
        _Instance = this;
        playerStatus = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<PlayerStatus>();
        headName = transform.Find("Name").GetComponent<UILabel>();
        hpBar = transform.Find("Hp").GetComponent<UISlider>();
        mpBar = transform.Find("Mp").GetComponent<UISlider>();
        hpLabel = transform.Find("Hp/Thumb/Label").GetComponent<UILabel>();
        mpLabel = transform.Find("Mp/Thumb/Label").GetComponent<UILabel>();
    }

    private void Start()
    {
        UpdateShow();
    }

    void Update()
    {
        UpdateShow();
    }

    public void UpdateShow()
    {
        headName.text = "Lv." + playerStatus.level + " " + playerStatus.name;
        hpBar.value = playerStatus.hpRemain / playerStatus.hp;
        mpBar.value = playerStatus.mpRemain / playerStatus.mp;
        hpLabel.text = playerStatus.hpRemain + "/" + playerStatus.hp;
        mpLabel.text = playerStatus.mpRemain + "/" + playerStatus.mp;
    }
}
