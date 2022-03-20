using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlotStore : SlotUI
{
    public Item myItem;
    public TextMeshProUGUI priceText;

    private void Start()
    {
        if (myItem != null)
        {
            FirstSetUp();
        }
    }

    void FirstSetUp() {
        icon.sprite = myItem.icon;
        priceText.text = myItem.price.ToString()+"$";
    }

    public override void OnClickSlot() {
        StorePage storePage = StorePage.instance;
        storePage.Buy(myItem);
    }

    
}
