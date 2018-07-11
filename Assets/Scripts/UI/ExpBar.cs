using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBar : MonoBehaviour
{
    public static ExpBar _Instance;
    private UISlider slider;

    private void Awake()
    {
        _Instance = this;
        slider = GetComponent<UISlider>();
    }

    public void SetValue(float value)
    {
        slider.value = value;
    }
}
