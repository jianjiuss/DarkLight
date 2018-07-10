using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour 
{
    private Camera miniMapCamera;
    private GameObject player;

    void Awake()
    {
        miniMapCamera = GameObject.FindGameObjectWithTag(Tags.miniMapCamera).GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag(Tags.player);
    }

    void Update()
    {
        miniMapCamera.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
    }

    public void OnZoomInClick()
    {
        if (miniMapCamera.orthographicSize <= 1)
        {
            return;
        }
        miniMapCamera.orthographicSize--;
    }

    public void OnZoomOutClick()
    {
        miniMapCamera.orthographicSize++;
    }
}
