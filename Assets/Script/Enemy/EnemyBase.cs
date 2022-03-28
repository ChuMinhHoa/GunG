using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStage {Idle, Move, Attack, Thinking}
public class EnemyBase : Actor
{
    public StateMachine<EnemyBase> StateMachine { get { return m_StateMachine; } }
    protected StateMachine<EnemyBase> m_StateMachine;

    [Header("=========ENEMY BASE============")]
    [Space(40)]
    public EnemyDataAsset enemyDataAsset;
    public EnemyStage enemyStage;
    public Vector3 spawnPosition;
    public Transform handTakeWeapon;
    public bool loseWeapon;

    public override void Awake()
    {
        base.Awake();
        InitStateMachine();
    }
    private void Update()
    {
        m_StateMachine.Update();
    }
    protected virtual void InitStateMachine() {
        m_StateMachine = new StateMachine<EnemyBase>(this);
        m_StateMachine.SetCurrentState(EnemyIdle.Instance);
    }
    public virtual void OnInit() {
        spawnPosition = transform.localPosition;
        StateMachine.ChangeState(EnemyIdle.Instance);
        ChangeProperty(
            enemyDataAsset.property.hp, 
            enemyDataAsset.property.hpMax, 
            enemyDataAsset.property.stamina,
            enemyDataAsset.property.shield,
            enemyDataAsset.property.shieldMax,
            enemyDataAsset.property.speed
        );
        actorType = enemyDataAsset.actorType;
        listLayerDamaged = enemyDataAsset.listLayerDamaged;
        timeReUpHpSetting = enemyDataAsset.timeReUpHpSetting;
        recovery_Ability = enemyDataAsset.recovery_Ability;
        SettingWeapons();
    }

    public virtual void ChangeProperty(
        float hp, float hpMax, float stamina, float shield, float shieldMax, float speed) {
        property.hp = hp;
        property.hpMax = hpMax;
        property.stamina = stamina;
        property.shield = shield;
        property.shieldMax = shieldMax;
        property.speed = speed;
    }

    public virtual void SettingWeapons() {
        if (enemyDataAsset.weapons.Count > 0)
            for (int i = 0; i < enemyDataAsset.weapons.Count; i++)
            {
                GameObject weaponsObject=Instantiate(enemyDataAsset.weapons[i], handTakeWeapon.position, Quaternion.identity);
                weaponsObject.transform.SetParent(handTakeWeapon);
                weapons.Add(weaponsObject.GetComponent<Weapon>());
                weapons[weapons.Count - 1].owner = this;
            }
        ResetEquipWeapon();
    }
    public override void SwitchWeapon(int weaponIndex)
    {
        base.SwitchWeapon(weaponIndex);
        ResetEquipWeapon();
    }

    public virtual void ResetEquipWeapon() {
        if (weapons.Count>0)
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                if (i > 0)
                {
                    weapons[i].gameObject.SetActive(false);
                }
            }
            currentWeapon = weapons[0];
            weapons[0].gameObject.SetActive(true);
        }
    }
    #region EnemyIdle
    public virtual void EnemyIdleStart() { }
    public virtual void EnemyIdleExecute() { }
    public virtual void EnemyIdleEnd() { }
    #endregion
    #region EnemyMove
    public Vector3 target;
    public float distanceToStop;
    public virtual void EnemyMoveStart() { }
    public virtual void EnemyMoveExecute() { }
    public virtual void EnemyMoveEnd() { }
    #endregion
    #region EnemyThink
    public float timeDecisionSetting;
    public float timeDecision;
    public bool minusTimeDecision;
    public bool followPLayer;
    public bool canAttack;
    public virtual void EnemyThinkStart() { }
    public virtual void EnemyThinkExecute() { }
    public virtual void EnemyThinkEnd() { }
    #endregion
    #region EnemyAttack
    public virtual void EnemyAttackStart() { }
    public virtual void EnemyAttackExecute() { }
    public virtual void EnemyAttackEnd() { }
    #endregion

    #region=============Check Player====================
    public float radius_Check_Player;
    public LayerMask whatIsPlayer;
    public bool playerOnView;
    public virtual bool CheckPLayerTarget()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius_Check_Player, whatIsPlayer);
        if (hit != null)
        {
            target = hit.transform.position;
            distanceToStop = 7f;
        }
        else if (hit == null && playerOnView)
            ResetAllBool();
        return hit != null;
    }
    public float rangeRandom;
    public virtual Vector3 RandomPosition() {
        float x = spawnPosition.x + Random.Range(-rangeRandom, rangeRandom);
        float y = spawnPosition.y + Random.Range(-rangeRandom, rangeRandom);
        Vector3 posRandom = new Vector3(x, y, 0);
        return posRandom;
    }
    
    public virtual void OnDrawGizmos()
    {
        if (playerOnView)
            Gizmos.color = Color.green;
        else Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius_Check_Player);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceToStop);
    }
    #endregion

    public virtual bool CheckDistanceToStop() {
        float distance = Vector2.Distance(transform.position, target);
        return distance <= distanceToStop;
    }

    public virtual bool CheckCanAttack() {
        return CheckDistanceToStop() && playerOnView;
    }
    public virtual void ResetAllBool() {
        playerOnView = false;
        canAttack = false; 
        followPLayer = false;
        StateMachine.ChangeState(EnemyIdle.Instance);
    }
}


