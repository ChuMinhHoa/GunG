using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{

    [Space(40)]
    [Header("======PLAYER ACTOR=======")]
    [Space(40)]
    public Transform myAim;
    public Vector2 movement;
    float angle;
    Gun gun;
    public Transform catchPointSpawn;
    public GameObject catchGameobject;

    [Header("---------KEY NUMBERS----------")]
    [Space(20)]
    public List<KeyCode> numberKeys;


    // Update is called once per frame
    void Update()
    {
        InputHandle();
        InputKeyHandle();
    }

    void InputHandle() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (currentWeaponType == WeaponType.Gun)
        {
            gun = currentWeapon.GetComponent<Gun>();
            if (gun.type == GunType.Rife)
            {
                RifleShot();
            }
            else {
                PistolAndShortGunShot();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Catch();
        }
    }

    void InputKeyHandle() {
        for (int i = 0; i < numberKeys.Count; i++)
        {
            if (weapons[i] != null && Input.GetKeyDown(numberKeys[i]))
            {
                SwitchWeapon(i);
            }
        }
    }

    public override void SwitchWeapon(int weaponIndex)
    {
        base.SwitchWeapon(weaponIndex);

        for (int i = 0; i < myAim.childCount; i++)
            myAim.GetChild(i).gameObject.SetActive(false);
        weapons[weaponIndex].gameObject.SetActive(true);
        currentWeapon = weapons[weaponIndex];
        currentWeaponType = currentWeapon.weaponType;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        Move();
        Rotage();
    }

    void RifleShot() {
        if (Input.GetMouseButton(0))
        {
            if (currentWeapon.Shot(angle, 7))
                myAnim.SetTrigger("Shot");
        }
    }

    void PistolAndShortGunShot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (currentWeapon.Shot(angle, 7))
                myAnim.SetTrigger("Shot");
        }
    }

    void Catch() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject created = Instantiate(catchGameobject, catchPointSpawn.position, Quaternion.identity);
        Catch createdCatch = created.GetComponent<Catch>();
        createdCatch.playerActor = this;
        createdCatch.target = mousePos;
        createdCatch.spawn = catchPointSpawn;
    }

    void Rotage() {

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);

    }
    public override void Move()
    {
        if (movement != Vector2.zero)
        {
            myBody.MovePosition(myBody.position + movement * property.speed * Time.deltaTime);
        }
    }

    public override void ChangeWeapon(Weapon weapon)
    {
        base.ChangeWeapon(weapon);

        if (myAim.childCount > 0)
        {
            ResetAllWeapon(currentWeapon.weaponController.weaponIndex);
        }

        weapon.transform.SetParent(myAim);
        weapon.transform.localPosition = new Vector3(0, 0, 0);
        weapon.transform.localRotation = Quaternion.identity;
    }

    void ResetAllWeapon(int indexGunChange) {
        for (int i = 0; i < myAim.childCount; i++)
            myAim.GetChild(i).gameObject.SetActive(false);
    }
}
