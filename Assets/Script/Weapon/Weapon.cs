using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Actor owner;
    public float damage;
    public WeaponType weaponType;
    public Weapon_Item weaponController;

    public bool changeOwner;
    public List<int> layerOwner;

    public virtual void Start() {
        if (weaponController.icon != null)
            GetComponent<SpriteRenderer>().sprite = weaponController.icon;

        damage = weaponController.damage;
        weaponType = weaponController.type;
    }

    public virtual void ChangeOwner() {
        if (owner != null)
        {
            owner.weapons.Remove(this);
            owner.currentWeapon = null;
            owner.SwitchWeapon(0);
        }
        
        changeOwner = true;
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (layerOwner.IndexOf(collision.gameObject.layer) != -1 && owner == null)
        {
            Actor targetActor = collision.GetComponent<Actor>();
            ChangeOwner();
            owner = targetActor;
            targetActor.ChangeWeapon(this);
            changeOwner = false;
        }

        if (!changeOwner)
            return;
    }

    public virtual bool Shot(float angle, int layerBullet) {
        return false;
    }
    public virtual void ResetWeapon() {
        owner.currentWeapon = null;
        owner.currentWeaponType = WeaponType.None;
    }
}

public enum WeaponType { 
    None,
    Melee,
    Gun,
    Grenade
}
