using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionBar : MonoBehaviour 
{
    public void OnStatusButtonClick()
    {}

    public void OnBagButtonClick()
    {
        if (Inventory._Instance.isInventoryShow)
        {
            Inventory._Instance.Hide();
        }
        else
        {
            Inventory._Instance.Show();
        }
    }

    public void OnEquipButtonClick()
    {}

    public void OnSkillButtonClick()
    {}

    public void OnSettingButtonClick()
    {}
}
