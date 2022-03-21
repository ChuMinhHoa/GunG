using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{

    public override void Start()
    {
        base.Start();
    }

    public override void ChangeHp(float damage, bool showPro)
    {
        base.ChangeHp(damage, showPro);
        if (showPro)
            OnChangeUI();
    }

    public override void ChangeShield(float value, bool showPro)
    {
        base.ChangeShield(value, showPro);
        if(showPro)
            OnChangeUI();
    }

    public virtual void OnChangeUI() {
        UI_EnemyManager.instance.ShowEnemyProperty();
        UI_EnemyManager.instance.ChangeHP(ActorName, property.hpMax, property.hp);
        UI_EnemyManager.instance.ChangeShield(ActorName, property.shieldMax, property.shield);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}
