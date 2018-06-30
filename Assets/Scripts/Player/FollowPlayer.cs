using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour 
{
    private Transform playerTransform;
    private Vector3 offset;
    private bool isRightButtonDown = false;
    public float distance = 0;
    public float scrollSpeed = 1;
    public float XRotateSpeed = 1;

	void Start () 
    {
        playerTransform = GameObject.FindGameObjectWithTag(Tags.player).transform;
        offset = transform.position - playerTransform.position;
        transform.LookAt(playerTransform.position);
	}

	void Update () 
    {
        this.transform.position = playerTransform.position + offset;
        ScrollView();
        RotateView();
	}

    void ScrollView()
    {
        distance = offset.magnitude;
        distance -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        distance = Mathf.Clamp(distance, 4, 14);
        offset = offset.normalized * distance;
    }

    void RotateView()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isRightButtonDown = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isRightButtonDown = false;
        }

        if (isRightButtonDown)
        {
            transform.RotateAround(playerTransform.position, playerTransform.up, Input.GetAxis("Mouse X") * XRotateSpeed);

            Vector3 originalPosition = transform.position;
            Quaternion originalRotate = transform.rotation;

            transform.RotateAround(playerTransform.position, transform.right, -Input.GetAxis("Mouse Y") * XRotateSpeed);

            float angleX = transform.eulerAngles.x;
            if (angleX < 10 || 80 < angleX)
            {
                transform.position = originalPosition;
                transform.rotation = originalRotate;
            }


            offset = transform.position - playerTransform.position;
        }

    }
}
