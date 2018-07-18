using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour 
{
    public static CursorManager _Instance;

    public Texture2D cursor_normal;
    public Texture2D cursor_npc_talk;
    public Texture2D cursor_attack;
    public Texture2D cursor_lockTarget;
    public Texture2D cursor_pick;

    private Vector2 hotspot = Vector2.zero;
    private CursorMode mode = CursorMode.Auto;

    private bool isSetLockTarget = false;
    void Start()
    {
        _Instance = this;
    }

    public void SetNormal()
    {
        isSetLockTarget = false;
        Cursor.SetCursor(cursor_normal, hotspot, mode);
    }

    public void SetNpcTalk()
    {
        if(isSetLockTarget)
        {
            return;
        }
        Cursor.SetCursor(cursor_npc_talk, hotspot, mode);
    }

    public void SetAttack()
    {
        if(isSetLockTarget)
        {
            return;
        }
        Cursor.SetCursor(cursor_attack, hotspot, mode);
    }

    public void SetLockTarget()
    {
        isSetLockTarget = true;
        Cursor.SetCursor(cursor_lockTarget, hotspot, mode);
    }
}

