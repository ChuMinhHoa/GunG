using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_EnemyManager : MonoBehaviour
{
    public static UI_EnemyManager instance;
    public UI_BarController healBar;
    public TextMeshProUGUI nameText;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        
        nameText.text = "";
    }

    public void ChangeHP(string actorName, float _currentValue, float _maxValue)
    {
        healBar = UI_BarManager.instance.GetBarController(BarName.E_Healthbar);
        healBar.OnChangeValue(_maxValue, _currentValue);
        nameText.text = actorName;
    }
}
