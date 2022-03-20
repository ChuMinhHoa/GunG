using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenage : Weapon
{
    public GameObject GranadePrefab;
    public override void Start()
    {
        base.Start();
    }

    public override bool Shot(float angle, int layerBullet)
    {
        GameObject granadeGameObject = Instantiate(GranadePrefab, transform.position, Quaternion.identity);
        granadeGameObject.transform.eulerAngles = new Vector3(0, 0, angle);
        Destroy(gameObject);
        return true;
    }
}
