using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item/New Item")]
public class Item : ScriptableObject
{
    public PropertyType propertyType;
    public string name;
    public Sprite icon;
    public int price;
}
