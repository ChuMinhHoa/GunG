using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>
{
    private T m_Owner;
    private State<T> m_CurrentState;
    public StateMachine(T owner) {
        m_Owner = owner;
        m_CurrentState = null;
    }
    public void SetCurrentState(State<T> s) { m_CurrentState = s; }
    public void Update() {
        if (m_CurrentState != null) m_CurrentState.Execute(m_Owner);
    }
    public void ChangeState(State<T> newState) {
        m_CurrentState.End(m_Owner);
        m_CurrentState = newState;
        m_CurrentState.Enter(m_Owner);
    }
}
