using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrugNpc : MonoBehaviour 
{
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DrugShop._Instance.SwitchDrugShop();
        }
    }
}
