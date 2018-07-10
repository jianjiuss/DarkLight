using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponNpc : MonoBehaviour 
{
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            WeaponShop._Instance.SwitchWeaponShow();
        }
    }
}
