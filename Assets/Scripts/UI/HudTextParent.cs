using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudTextParent : MonoBehaviour 
{
    public static HudTextParent _Instance;

    void Awake()
    {
        _Instance = this;
    }
}
