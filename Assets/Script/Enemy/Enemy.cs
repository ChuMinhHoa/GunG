using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{

    public override void Start()
    {
        base.Start();
    }

    public override void OnChangeUI() {
        UI_EnemyManager.instance.ShowEnemyProperty();
        UI_EnemyManager.instance.ChangeHP(ActorName, property.hpMax, property.hp);
        UI_EnemyManager.instance.ChangeShield(ActorName, property.shieldMax, property.shield);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
