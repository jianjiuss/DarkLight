using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShop : MonoBehaviour 
{
    public static WeaponShop _Instance;

    public GameObject WeaponItemPrefabs;

    private int[] weaponObjectIds;
    private TweenPosition tweenPosition;
    private bool isShow;
    private UIGrid grid;
    private GameObject gridGo;
    private UIScrollView scrollView;

    void Awake()
    {
        _Instance = this;
        tweenPosition = GetComponent<TweenPosition>();
        gridGo = transform.Find("Scroll View/Grid").gameObject;
        grid = gridGo.GetComponent<UIGrid>();
        scrollView = GetComponentInChildren<UIScrollView>();
        weaponObjectIds = ObjectsInfo._Instance.GetObjectInfos(ObjectType.Equip);
    }

    void Start()
    {
        UpdateShow();
    }

    public void SwitchWeaponShow()
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

    public void OnCloseButtonClick()
    {
        SwitchWeaponShow();
    }

    private void UpdateShow()
    {
        for(int i = 0; i < weaponObjectIds.Length; i++)
        {
            GameObject go = NGUITools.AddChild(gridGo, WeaponItemPrefabs);
            grid.AddChild(go.transform);
            go.transform.localPosition = Vector3.zero;
            go.GetComponent<WeaponItem>().SetId(weaponObjectIds[i]);
            go.GetComponent<UIDragScrollView>().scrollView = scrollView; 
        }
    }
}
