using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShortGun : Bullet
{

    public float angle;
    public float recoil;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();
        Bullet[] bullets = GetComponentsInChildren<Bullet>();
        for (int i = 1; i < bullets.Length; i++)
        {
            bullets[i].myGun = myGun;
            bullets[i].transform.eulerAngles = new Vector3(0, 0, RandomAngle() + angle);
            bullets[i].damage = damage;
            bullets[i].gameObject.layer = gameObject.layer;
        }
    }

    public override void FixedUpdate()
    {

    }

    float RandomAngle()
    {
        float _recoil = Random.Range(-recoil, recoil);
        return _recoil;
    }
}
