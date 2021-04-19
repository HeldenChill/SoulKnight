using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarControl : MonoBehaviour
{
    [SerializeField]private Slider slider;
    [SerializeField]private Text text;
    int maxValue;

    public void setMaxValue(int maxValue){
        this.maxValue = maxValue;
        slider.maxValue = maxValue;
        slider.value = maxValue;
        text.text = maxValue.ToString()+ "/" + maxValue.ToString();
    }

    public void setValue(int value){
        slider.value = value;
        text.text = value.ToString()+ "/" + maxValue.ToString();
    }
}
