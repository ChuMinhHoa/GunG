using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour
{

    public Image imageFill;
    public float maxValue;
    public float currentValue;


    public void ChangeValue(float value)
    {
        currentValue = value;
        imageFill.fillAmount = currentValue / maxValue;
    }

    public void SetMaxValue(float value) {
        maxValue = value;
    }

}
