using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BarName { E_Healthbar, E_StaminaBar, P_Healthbar}
public class UI_BarController : MonoBehaviour
{
    public float current_Value;
    public float max_Value;
    public Image fillBar;
    public BarName barName;
    UI_BarManager uI_BarManager;

    public virtual void Start() {
        uI_BarManager = UI_BarManager.instance;
        OnInit(1, 1);
    }
    public virtual void OnInit(float _currentValue, float _maxValue) {
        current_Value = _currentValue;
        max_Value = _maxValue;
        OnChangeValue(1,1);
        uI_BarManager.AddBarController(this);
    }
    public virtual void OnOpen() {
        gameObject.SetActive(true);
    }
    public virtual void OnClose() {
        gameObject.SetActive(false);
    }
    public virtual void OnChangeValue(float _currentValue, float _maxValue) {
        current_Value = _currentValue;
        max_Value = _maxValue;
        fillBar.fillAmount = current_Value / max_Value;
    }
}
