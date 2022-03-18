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

    public virtual void Start() {
        if (weaponController.icon != null)
            GetComponent<SpriteRenderer>().sprite = weaponController.icon;

        damage = weaponController.damage;
        weaponType = weaponController.type;
    }

    public virtual void ChangeOwner(Actor newOwner) {
        owner = newOwner;
        changeOwner = true;
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
            ChangeOwner(collision.GetComponent<Actor>());

        if (!changeOwner)
            return;

        if (collision.GetComponent<Actor>())
        {
            Actor targetActor = collision.GetComponent<Actor>();
            if (targetActor == owner)
            {
                targetActor.ChangeWeapon(this);
                changeOwner = false;
            }
           
        }
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
    Gun
}
