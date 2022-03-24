using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyStage {Idle, Move, Attack, Thinking }
public class EnemyBase : Actor
{
    public StateMachine<EnemyBase> StateMachine { get { return m_StateMachine; } }
    protected StateMachine<EnemyBase> m_StateMachine;

    [Header("==================ENEMY============")]
    [Space(40)]
    public EnemyDataAsset enemyDataAsset;
    public EnemyStage enemyStage;

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
        StateMachine.ChangeState(EnemyIdle.Instance);
        property = enemyDataAsset.property;
        actorType = enemyDataAsset.actorType;
        listLayerDamaged = enemyDataAsset.listLayerDamaged;
        timeReUpHpSetting = enemyDataAsset.timeReUpHpSetting;
        recovery_Ability = enemyDataAsset.recovery_Ability;
        SettingWeapons();
    }

    public virtual void SettingWeapons() {
        if (enemyDataAsset.weapons.Count > 0)
            for (int i = 0; i < enemyDataAsset.weapons.Count; i++)
            {
                weapons.Add(enemyDataAsset.weapons[i].GetComponent<Weapon>());
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

    #region=============CHeck Player====================
    public float radius_Check_Player;
    public LayerMask whatIsPlayer;
    public bool playerOnView;
    public virtual bool CheckPLayerTarget()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius_Check_Player, whatIsPlayer);
        if (hit != null)
            target = hit.transform.position;
        else if (hit==null && playerOnView)
        {
            target = Vector3.zero;
            ResetAllBool();
        }
        return hit != null;
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
    public virtual void ResetAllBool() {
        playerOnView = false;
        canAttack = false; 
        followPLayer = false;
        StateMachine.ChangeState(EnemyIdle.Instance);
    }
}


