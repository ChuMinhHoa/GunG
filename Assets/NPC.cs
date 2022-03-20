using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public bool onTarget;
    public float radius;
    public LayerMask whatIsPlayer;
    public GameObject guildeUI;
    public virtual void Update() {
        PlayerOnTarget();
        if (onTarget && Input.GetKeyDown(KeyCode.E))
            OnTrigger();
    }
    public void PlayerOnTarget() {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, whatIsPlayer);
        if (hit != null)
        {
            onTarget = true;
            guildeUI.SetActive(true);
            return;
        }
        OnExit();
        onTarget = false;
    }
    public virtual void OnTrigger() { }
    public virtual void OnExit() {
        guildeUI.SetActive(false);
    }

    public virtual void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
