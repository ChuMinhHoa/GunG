using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_PlayerManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public UI_BarController healBar;
    public UI_BarController shieldBar;
    public static UI_PlayerManager instance;
    private void Awake()
    {
        instance = this;
    }
    public void OnChangePropertyOfPlayer()
    {
        healBar = UI_BarManager.instance.GetBarController(BarName.P_Healthbar);
        shieldBar = UI_BarManager.instance.GetBarController(BarName.P_ShieldBar);
        healBar.OnOpen();
        shieldBar.OnOpen();
    }
    public void ChangeHP(string actorName, float _currentValue, float _maxValue)
    {
        healBar.OnChangeValue(_maxValue, _currentValue);
        nameText.text = actorName;
    }
    public void ChangeShield(string actorName, float _currentValue, float _maxValue)
    {
        shieldBar.OnChangeValue(_maxValue, _currentValue);
        nameText.text = actorName;
    }
}
