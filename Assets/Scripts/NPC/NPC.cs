using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour 
{
    void OnMouseEnter()
    {
        CursorManager._Instance.SetNpcTalk();
    }

    void OnMouseExit()
    {
        CursorManager._Instance.SetNormal();
    }
}
