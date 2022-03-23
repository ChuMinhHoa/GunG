using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_Item : ItemBase
{
    public override void OnUse()
    {
        base.OnUse();
        myActor.ChangeShield(itemDataAsset.propertyChange, true);
    }
}
