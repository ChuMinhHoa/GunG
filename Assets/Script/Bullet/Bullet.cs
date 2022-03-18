using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    Rigidbody2D myBody;
    public float timeLife = 2f;

    public Gun myGun;
    public float damage;

    public virtual void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    public virtual void Start()
    {
        Destroy(gameObject, timeLife);
    }


    public virtual void FixedUpdate()
    {
        myBody.velocity = transform.right * speed;
    }
}
