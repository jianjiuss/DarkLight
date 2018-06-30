﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDir : MonoBehaviour 
{
    public GameObject clickEffectPrefabs;
    public Vector3 targetPosition = Vector3.zero;
    private bool isMouseLeftButtonDown = false;
    private PlayerMove playerMove;

    void Start()
    {
        playerMove = GetComponent<PlayerMove>();
        targetPosition = transform.position;
    }

	void Update () 
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit) && raycastHit.collider.tag.Equals(Tags.ground))
            {
                isMouseLeftButtonDown = true;
                ShowClickEffect(raycastHit.point);
                LookAtPosition(raycastHit.point);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isMouseLeftButtonDown = false;
        }

        if (isMouseLeftButtonDown)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit) && raycastHit.collider.tag.Equals(Tags.ground))
            {
                LookAtPosition(raycastHit.point);
            }
        }
        else
        {
            if (playerMove.isMoving)
            {
                LookAtPosition(targetPosition);
            }
        }
	}

    void ShowClickEffect(Vector3 position)
    {
        position = new Vector3(position.x, position.y + 0.1f, position.z);
        GameObject.Instantiate(clickEffectPrefabs, position, Quaternion.identity);
    }

    void LookAtPosition(Vector3 position)
    {
        targetPosition = new Vector3(position.x, transform.position.y, position.z);
        transform.LookAt(targetPosition);
    }
}
