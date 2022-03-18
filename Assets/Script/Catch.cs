using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catch : MonoBehaviour
{
    public LineRenderer lineCatch;
    public GameObject endP;
    public GameObject starP;
    public Vector3 target;

    public float speed;

    public Transform spawn;
    public Vector3 spawnPoint;
    public float maxDistance;

    public LayerMask whatCanCatch;
    public float radius;

    public Actor playerActor;

    private void Start()
    {
        lineCatch.SetPosition(0, spawn.position);
        lineCatch.SetPosition(1, spawn.position);
        spawnPoint = transform.position;
    }

    private void FixedUpdate()
    {
       

        float distance = Vector3.Distance(transform.position, spawnPoint);

        CatchCheck();

        if (distance < maxDistance && transform.position != target)
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        else
        {
            target = spawn.position;
            Destroy(gameObject, .25f);
        
        }

        starP.transform.position = spawn.position;
        lineCatch.SetPosition(0, spawn.position);
        lineCatch.SetPosition(1, transform.position);
        endP.transform.position = transform.position;
    }

    void CatchCheck() {
        Collider2D hit = Physics2D.OverlapCircle(endP.transform.position, radius, whatCanCatch);
        if (hit != null)
        {
            target = spawn.position;
            if (hit.GetComponent<Weapon>())
            {
                Weapon targetWeapon = hit.GetComponent<Weapon>();
                targetWeapon.ChangeOwner(playerActor);
            }
            
            hit.transform.SetParent(endP.transform);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(endP.transform.position, radius);
    }
}
