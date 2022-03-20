using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{

    [Header("---------PROPERTY--------")]
    [Space(40)]
    [Header("=========ACTOR==========")]
    public string ActorName;
    public Property property;

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
    //public BarController hp_Bar;

    public virtual void Awake() {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }

    public virtual void Start() {
        //if (hp_Bar!=null)
        //{
        //    hp_Bar.SetMaxValue(property.hpMax);
        //    hp_Bar.ChangeValue(property.hp);
        //}
    }

    public virtual void FixedUpdate()
    {
        if (!shotted && property.hp < property.hpMax)
        {
            if (timeReUpHp > 0f) timeReUpHp -= Time.deltaTime;
            if (timeReUpHp <= 0f)
            {
                curveScale += Time.deltaTime;
                ChangeHp(recovery_Ability * CurveAnimation.instance.list_Anim_Curves[0].animCurve.Evaluate(curveScale));
            }
        }
    }

    public virtual void ChangeHp(float damage) {
        property.hp += damage;
        if (property.hp > property.hpMax)
            property.hp = property.hpMax;
        //hp_Bar.ChangeValue(property.hp);
    }

    public void ChangeStamina(float sta)
    {
        property.stamina += sta;
    }

    public void ChangeShield(float value) {
        
        if (property.shield <=0)
        {
            ChangeHp(value);
            return;
        }
        property.shield += value;
    }

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
            ChangeShield(-collision.gameObject.GetComponent<Bullet>().damage);
            shotted = true;
            if (property.hp < property.hpMax)
                timeReUpHp = timeReUpHpSetting;
            StopAllCoroutines();
            StartCoroutine(ResetShotted());
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
}
