using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyKey : MonoBehaviour 
{
	private bool isPressAnyKey = false;
	private GameObject buttonContainer;

	void Start()
    {
        buttonContainer = this.gameObject.transform.parent.Find("ButtonContainer").gameObject;
	}

	void Update()
    {
        if (!isPressAnyKey)
        {
            if (Input.anyKey)
            {
                ShowButton();
            }
        }
	}

    void ShowButton()
    {
        this.gameObject.SetActive(false);
        buttonContainer.SetActive(true);
        isPressAnyKey = true;
    }
}
