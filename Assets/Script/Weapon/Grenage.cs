using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenage : Gun
{
    public override void Start()
    {
        base.Start();
    }

    public override bool Shot(float angle, int layerBullet)
    {
        base.Shot(angle, layerBullet);
        Destroy(gameObject);
        return true;
    }
}
