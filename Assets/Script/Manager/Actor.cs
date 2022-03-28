using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ActorType { Player, Enemy}
public class Actor : MonoBehaviour
{

    [Header("---------PROPERTY--------")]
    [Space(40)]
    [Header("=========ACTOR==========")]
    public string ActorName;
    public Property property;
    public ActorType actorType;

    [Header("------WEAPON CONTROL-----")]
    [Space(20)]
    public WeaponType currentWeaponType;
    public Weapon currentWeapon;
    public List<Weapon> weapons;

    [Header("-----PHYSICS AND ANIM------")]
    [Space(20)]
    public Rigidbody2D myBody;
    public Animator myAnim;
    public List<int> listLayerDamaged;

    [Header("-----UI AND PROPERTYCHANGE------")]
    [Space(20)]
    public float timeReUpHpSetting;
    public float recovery_Ability;
    float timeReUpHp;
    bool shotted;
    float curveScale = 0f;

    public virtual void Awake() {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    public virtual void Start() {
        
    }

    public virtual void FixedUpdate()
    {
        if (!shotted && property.hp < property.hpMax)
        {
            if (timeReUpHp > 0f) timeReUpHp -= Time.deltaTime;
            if (timeReUpHp <= 0f)
            {
                curveScale += Time.deltaTime;
                ChangeHp(
                    recovery_Ability * CurveAnimation.instance.list_Anim_Curves[0].animCurve.Evaluate(curveScale),
                    actorType == ActorType.Player ? true : false);
            }
        }
    }

    public virtual void ChangeHp(float damage, bool showPro) {
        property.hp += damage;
        if (showPro)
            OnChangeUI();
        if (property.hp > property.hpMax)
            property.hp = property.hpMax;
    }

    public void ChangeStamina(float sta)
    {
        property.stamina += sta;
    }

    public virtual void ChangeShield(float value, bool showPro) {

        property.shield += value;
        if (showPro)
            OnChangeUI();
        if (property.shield <=0 && value < 0)
        {
            property.shield = 0;
            ChangeHp(value , true);
            return;
        }
    }
    public virtual void OnChangeUI() {}
    public virtual void ChangeWeapon(Weapon weapon) {
        currentWeapon = weapon;
        currentWeaponType = currentWeapon.weaponType;

        int weaponIndex = currentWeapon.weaponController.weaponIndex;
        if (weapons[weaponIndex] != null)
            Destroy(weapons[weaponIndex].gameObject);
        weapons[weaponIndex] = currentWeapon;
        Destroy(weapon.GetComponent<Collider2D>());
    }
    public virtual void Move() { }
    public virtual void SwitchWeapon(int weaponIndex) { }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (listLayerDamaged.IndexOf(collision.gameObject.layer) != -1)
        {
            Bullet e_Bullet = collision.gameObject.GetComponent<Bullet>();
            ChangeShield(-e_Bullet.damage, true);
            shotted = true;
            if (property.hp < property.hpMax)
                timeReUpHp = timeReUpHpSetting;
            StopAllCoroutines();
            StartCoroutine(ResetShotted());
            if (e_Bullet.can_Destroy)
                Destroy(collision.gameObject);
        }
    }

    public virtual IEnumerator ResetShotted() {
        yield return new WaitForSeconds(.5f);
        shotted = false;
        curveScale = 0f;
    }

}

[System.Serializable]
public class Property {
    public float hp;
    public float hpMax;
    public float stamina;
    public float speed;
    public float shield;
    public float shieldMax;
}
