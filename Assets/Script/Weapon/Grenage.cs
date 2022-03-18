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
        Instantiate(GranadePrefab, transform.position, Quaternion.identity);
        return true;
    }
}
