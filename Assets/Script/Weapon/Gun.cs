using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public int bulletCount;
    public Transform shotPoint;
    public GameObject bullet;
    private float timeResetshot;
    public float timeResetshotSetting;
    public GunType type;
    public float recoil;
    public float intensity;

    Vector2 movement;

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (timeResetshot > 0f)
            timeResetshot -= Time.deltaTime;
    }

    public override bool Shot(float angle, int layerBullet) {

        if (timeResetshot <= 0f)
        {
            CameraCinemachineController.instance.CameraShake(intensity);
            if (type == GunType.ShortGun) ShortGun(angle, layerBullet);
            else Rifle(angle, layerBullet);
            return true;
        }

        return false;
    }

    float RandomAngle() {
        float _recoil = 0f;
        if (movement != Vector2.zero)
            _recoil = Random.Range(-recoil * (movement.x + movement.y), recoil * (movement.x + movement.y));
        else
            _recoil = Random.Range(-recoil , recoil);
        return _recoil;
    }

    public void Rifle(float angle, int layerBullet) {
        GameObject created = Instantiate(bullet, shotPoint.position, Quaternion.identity);
        created.transform.eulerAngles = new Vector3(0, 0, angle + RandomAngle());
        bulletCount--;
        created.layer = layerBullet;
        Bullet bulletCreated = created.GetComponent<Bullet>();
        bulletCreated.myGun = this;
        timeResetshot = timeResetshotSetting;
        if (bulletCount <= 0)
        {
            ResetWeapon();
            Destroy(gameObject);
        }
    }

    public void ShortGun(float angle, int layerBullet) {
        GameObject created = Instantiate(bullet, shotPoint.position, Quaternion.identity);
        created.transform.eulerAngles = new Vector3(0, 0, angle + RandomAngle());
        created.GetComponent<BulletShortGun>().angle = angle;
        bulletCount--;
        created.layer = layerBullet;
        Bullet bulletCreated = created.GetComponent<Bullet>();
        bulletCreated.myGun = this;
        timeResetshot = timeResetshotSetting;
        if (bulletCount <= 0)
        {
            ResetWeapon();
            Destroy(gameObject);
        }
    }
}

public enum GunType { Rife, ShortGun, Pistol }
