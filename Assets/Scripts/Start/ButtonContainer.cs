using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonContainer : MonoBehaviour 
{
    public void OnNewGame()
    {
        PlayerPrefs.SetInt("DataFromSave", 0);
    }

    public void OnLoadGame()
    {
        PlayerPrefs.SetInt("DataFromSave", 1);
    }
}
