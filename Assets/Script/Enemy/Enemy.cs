using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyBase
{
   
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
    }
    #region EnemyIdle;
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
        if (timeDecision > 0f && minusTimeDecision)
        {
            timeDecision -= Time.deltaTime;
        }

        if (timeDecision <= 0f)
            StateMachine.ChangeState(EnemyThinking.Instance);
    }
    public override void EnemyIdleEnd()
    {
        base.EnemyIdleEnd();
    }
    #endregion

    #region ===================ENEMY MOVE=================
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
            //Attack
            Vector2 movement = (target - transform.position).normalized;
            myBody.MovePosition(myBody.position + movement * property.speed * Time.deltaTime);
            
            if (CheckDistanceToStop())
            {
                nearTarget = true;
                if (playerOnView)
                {
                    canAttack = true;
                    StateMachine.ChangeState(EnemyThinking.Instance);
                    return;
                }
                StateMachine.ChangeState(EnemyIdle.Instance);
                return;
            }
        }
    }
    public override void EnemyMoveEnd()
    {
        base.EnemyMoveEnd();
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
        if (!CheckDistanceToStop())
        {
            followPLayer = false;
        }

        if (playerOnView && !followPLayer)
        {
            Debug.Log("Player OnView");
            StateMachine.ChangeState(EnemyMove.Instance);
            followPLayer = true;
            return;
        }

        if (canAttack)
        {
            Debug.Log("Attack");
            return;
        }

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
                    StateMachine.ChangeState(EnemyMove.Instance);
                    break;
                default:
                    break;
            }
        }
    }
    public override void EnemyThinkEnd()
    {
        base.EnemyThinkEnd();
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
