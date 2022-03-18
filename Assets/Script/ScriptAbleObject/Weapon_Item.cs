using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New weapon", menuName = "Item/Weapon")]
public class Weapon_Item : ScriptableObject
{
    public Sprite icon;
    public float damage;
    public WeaponType type;
    public int weaponIndex;
}
