using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyBase
{
    public float currentAngle;
    public List<LayerMask> whatIsWeapons;
    public override void Awake()
    {
        base.Awake();
    }
    public override void Start()
    {
        base.Start();
        OnInit();
    }
    public override void OnInit()
    {
        base.OnInit();
        enemyStage = EnemyStage.Thinking;
        StateMachine.ChangeState(EnemyThinking.Instance);
    }

    public override void OnChangeUI() {
        UI_EnemyManager.instance.ShowEnemyProperty();
        UI_EnemyManager.instance.ChangeHP(ActorName, property.hpMax, property.hp);
        UI_EnemyManager.instance.ChangeShield(ActorName, property.shieldMax, property.shield);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        playerOnView = CheckPLayerTarget();
        if (playerOnView && !followPLayer)
            StateMachine.ChangeState(EnemyThinking.Instance);
        if (playerOnView)
        {
            Rotage();
        }
    }
    #region EnemyIdle
    public override void EnemyIdleStart()
    {
        base.EnemyIdleStart();
        Debug.Log("Enemy Idle!");
        minusTimeDecision = true;
        enemyStage = EnemyStage.Idle;
    }
    public override void EnemyIdleExecute()
    {
        base.EnemyIdleExecute();
        
        StateMachine.ChangeState(EnemyThinking.Instance);
    }
    public override void EnemyIdleEnd()
    {
        base.EnemyIdleEnd();
    }
    #endregion

    #region ENEMY MOVE
    bool nearTarget;
    public override void EnemyMoveStart()
    {
        base.EnemyMoveStart();
        Debug.Log("Enemy Move!");
        nearTarget = false;
        enemyStage = EnemyStage.Move;
    }
    public override void EnemyMoveExecute()
    {
        base.EnemyMoveExecute();
        if (!nearTarget)
        {
            Move();
            Rotage();
        }
        else
            StateMachine.ChangeState(EnemyIdle.Instance);
    }
    public override void EnemyMoveEnd()
    {
        base.EnemyMoveEnd();
    }

    public override void Move() {
        Vector2 direction = (target - transform.position).normalized;
        myBody.MovePosition(myBody.position + direction * property.speed * Time.deltaTime);

        if (CheckDistanceToStop())
            nearTarget = true;
    }
    void Rotage() {
        Vector2 direction = target - transform.position;
        currentAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, currentAngle);
    }
    #endregion

    #region EnemyThinking
    public override void EnemyThinkStart()
    {
        base.EnemyThinkStart();
        enemyStage = EnemyStage.Thinking;
        Debug.Log("Enemy Thinking!");
    }
    public override void EnemyThinkExecute()
    {
        base.EnemyThinkExecute();
        Thinking();
    }
    public override void EnemyThinkEnd()
    {
        base.EnemyThinkEnd();
    }
    public virtual bool MinusTime() {

        if (timeDecision > 0f && minusTimeDecision)
        {
            timeDecision -= Time.deltaTime;
            return true;
        }
        return false;
    }
    public virtual void Thinking() {

        if (weapons.Count == 0)
        {
            StateMachine.ChangeState(EnemyFindWeapon.Instance);
            return;
        }
        if (!CheckDistanceToStop())
            followPLayer = false;

        if (playerOnView && !followPLayer)
        {
            StateMachine.ChangeState(EnemyMove.Instance);
            followPLayer = true;
            return;
        }

        canAttack = CheckCanAttack();
        if (canAttack)
        {
            StateMachine.ChangeState(EnemyAttack.Instance);
            return;
        }

        if (MinusTime())
            return;
        MakeDecision();
    }

    public virtual void MakeDecision() {
        if (timeDecision <= 0f)
        {
            int randomDecision = Random.Range(0, 2);
            timeDecision = timeDecisionSetting;
            minusTimeDecision = false;
            switch (randomDecision)
            {
                case 0:
                    StateMachine.ChangeState(EnemyIdle.Instance);
                    break;
                case 1:
                    target = RandomPosition();
                    distanceToStop = .1f;
                    StateMachine.ChangeState(EnemyMove.Instance);
                    break;
                default:
                    break;
            }
        }
    }
    #endregion

    #region EnemyAttack
    public override void EnemyAttackStart()
    {
        base.EnemyAttackStart();
    }
    public override void EnemyAttackExecute()
    {
        base.EnemyAttackExecute();
        canAttack = CheckCanAttack();
        if (currentWeapon != null && canAttack)
        {
            currentWeapon.Shot(currentAngle, 12);
        }
        else StateMachine.ChangeState(EnemyThinking.Instance);
    }
    public override void EnemyAttackEnd()
    {
        base.EnemyAttackEnd();
    }
    #endregion

    #region Enemy Find Weapon
    public List<Transform> weaponsFinded;
    public override void EnemyFindWeaponStart()
    {
        base.EnemyFindWeaponStart();
        Debug.Log("Enemy finding!");
        FindWeapons();
    }
    public override void EnemyFindWeaponExecute()
    {
        base.EnemyFindWeaponExecute();
        if (weaponsFinded.Count > 0)
            target = weaponsFinded[Random.Range(0, weaponsFinded.Count)].position;
        StateMachine.ChangeState(EnemyMove.Instance);
    }
    public override void EnemyFindWeaponEnd()
    {
        base.EnemyFindWeaponEnd();
    }
    public virtual void FindWeapons() { 
    
    }
    #endregion
}
public class EnemyIdle : State<EnemyBase>
{
    private static EnemyIdle m_Instance = null;
    public static EnemyIdle Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new EnemyIdle();
            }
            return m_Instance;
        }
    }
    public override void Enter(EnemyBase go)
    {
        go.EnemyIdleStart();
    }
    public override void Execute(EnemyBase go)
    {
        go.EnemyIdleExecute();
    }
    public override void End(EnemyBase go)
    {
        go.EnemyIdleEnd();
    }
}
public class EnemyMove : State<EnemyBase>
{
    private static EnemyMove m_Instance = null;
    public static EnemyMove Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new EnemyMove();
            }
            return m_Instance;
        }
    }
    public override void Enter(EnemyBase go)
    {
        go.EnemyMoveStart();
    }
    public override void Execute(EnemyBase go)
    {
        go.EnemyMoveExecute();
    }
    public override void End(EnemyBase go)
    {
        go.EnemyMoveEnd();
    }
}
public class EnemyThinking : State<EnemyBase>
{
    private static EnemyThinking m_Instance = null;
    public static EnemyThinking Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new EnemyThinking();
            }
            return m_Instance;
        }
    }
    public override void Enter(EnemyBase go)
    {
        go.EnemyThinkStart();
    }
    public override void Execute(EnemyBase go)
    {
        go.EnemyThinkExecute();
    }
    public override void End(EnemyBase go)
    {
        go.EnemyThinkEnd();
    }
}
public class EnemyAttack : State<EnemyBase>
{
    private static EnemyAttack m_Instance = null;
    public static EnemyAttack Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new EnemyAttack();
            }
            return m_Instance;
        }
    }
    public override void Enter(EnemyBase go)
    {
        go.EnemyAttackStart();
    }
    public override void Execute(EnemyBase go)
    {
        go.EnemyAttackExecute();
    }
    public override void End(EnemyBase go)
    {
        go.EnemyAttackEnd();
    }
}
public class EnemyFindWeapon : State<EnemyBase>
{
    private static EnemyFindWeapon m_Instance = null;
    public static EnemyFindWeapon Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = new EnemyFindWeapon();
            }
            return m_Instance;
        }
    }
    public override void Enter(EnemyBase go)
    {
        go.EnemyFindWeaponStart();
    }
    public override void Execute(EnemyBase go)
    {
        go.EnemyFindWeaponExecute();
    }
    public override void End(EnemyBase go)
    {
        go.EnemyFindWeaponEnd();
    }
}
