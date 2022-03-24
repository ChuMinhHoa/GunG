using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New EnemyDataAsset", menuName = "Enemy/New enemy")]
public class EnemyDataAsset : ScriptableObject
{
    public Property property;
    public ActorType actorType;
    public List<GameObject> weapons;
    public List<int> listLayerDamaged;
    public float timeReUpHpSetting;
    public float recovery_Ability;
}
