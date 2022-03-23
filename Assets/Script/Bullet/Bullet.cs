using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Rigidbody2D myBody;
    public float timeLife = 2f;

    public Gun myGun;
    public float damage;

    public bool can_Destroy;

    public virtual void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    public virtual void Start()
    {
        OnInit();
        if (timeLife == 0)
            return;
        Destroy(gameObject, timeLife);
    }

    public virtual void OnInit() {
        if (myGun!=null)
            damage = myGun.damage;
    }


    public virtual void FixedUpdate()
    {
        if (myGun!=null)
            myBody.velocity = transform.right * speed;
    }
}
