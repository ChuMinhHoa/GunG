using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputFieldCl : MonoBehaviour
{

    public TextMeshProUGUI placeholder;
    public TMP_InputField inputText;

    public Button cancelButton, confirmButton;
    public Item currentItemBuy;
    public int countItemBuy;

    BagManager bagController;
    BudgetManager budget;

    public DebugMode debugMode;

    private void Start()
    {
        confirmButton.onClick.AddListener(ConfirmButtonOnClick);
        cancelButton.onClick.AddListener(CancelButtonOnClick);

        bagController = BagManager.instance;
        budget = BudgetManager.instance;
        if (debugMode == DebugMode.On)
            Debug.unityLogger.logEnabled = true;
        else Debug.unityLogger.logEnabled = false;
    }

    void ConfirmButtonOnClick() {
        if (int.TryParse(inputText.text, out countItemBuy))
            countItemBuy = int.Parse(inputText.text);
        else countItemBuy = 99;

        if (countItemBuy > 99)
            countItemBuy = 99;

        if (CheckCoin(currentItemBuy.price * countItemBuy))
        {
            Debug.Log("Add item to bag controller!");
            budget.m_coin = -currentItemBuy.price * countItemBuy;
            bagController.AddItem(currentItemBuy, countItemBuy);
        }
        else Debug.Log("Can't buy!");

        CancelButtonOnClick();
    }

    void CancelButtonOnClick() {
        gameObject.SetActive(false);
        inputText.text = "";
    }


    bool CheckCoin(int price)
    {
        if (budget.m_coin >= price)
            return true;
        else return false;
    }
}
public enum DebugMode { On, Off}
