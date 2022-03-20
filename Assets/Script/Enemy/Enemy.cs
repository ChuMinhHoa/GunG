using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    UI_BarManager barManager;

    public override void Start()
    {
        base.Start();
    }

    public override void ChangeHp(float damage)
    {
        base.ChangeHp(damage);
        UI_EnemyManager.instance.ChangeHP(ActorName, property.hpMax, property.hp);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
