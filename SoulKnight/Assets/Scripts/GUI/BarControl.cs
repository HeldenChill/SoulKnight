using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarControl : MonoBehaviour
{
    [SerializeField]private Slider slider;
    [SerializeField]private Text text;
    float maxValue;

    public void setMaxValue(float maxValue){
        this.maxValue = maxValue;
        slider.maxValue = maxValue;
        slider.value = maxValue;
        if(text != null)
            text.text = maxValue.ToString()+ "/" + maxValue.ToString();
    }

    public void setValue(float value){
        slider.value = value;
        if(text != null)
            text.text = value.ToString()+ "/" + maxValue.ToString();
    }

    public float getValue(){
        return slider.value;
    }
}
