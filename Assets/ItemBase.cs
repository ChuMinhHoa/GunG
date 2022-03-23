using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public Item itemDataAsset;
    public Actor myActor;
    public virtual void Start() { }
    public virtual void OnInit() { }
    public virtual void OnUse() {
        if (myActor == null)
            return;
    }
    public virtual void OnChange(Actor whoIsOwner) {
        if (whoIsOwner == null)
            return;
    }
    public virtual Item GetItemDataAsset() {
        return itemDataAsset;
    }
}
