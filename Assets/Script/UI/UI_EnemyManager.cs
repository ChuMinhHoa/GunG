using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_EnemyManager : MonoBehaviour
{
    public static UI_EnemyManager instance;
    public UI_BarController healBar;
    public UI_BarController shieldBar;
    public TextMeshProUGUI nameText;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        nameText.text = "";
    }
    public void ShowEnemyProperty() {
        if (healBar==null && shieldBar==null)
        {
            healBar = UI_BarManager.instance.GetBarController(BarName.E_Healthbar);
            shieldBar = UI_BarManager.instance.GetBarController(BarName.E_ShieldBar);
            healBar.OnOpen();
            shieldBar.OnOpen();
            StartCoroutine(ResetBarController());
        }
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

    IEnumerator ResetBarController() {
        yield return new WaitForSeconds(1.5f);
        shieldBar.OnClose();
        healBar.OnClose();
        healBar = null;
        shieldBar = null;
    }
}
