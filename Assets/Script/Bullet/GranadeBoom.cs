using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeBoom : Bullet
{
    float timeScale = 0f;
    float angle = 0f;
    CurveAnimation curveAnimation;
    AnimationCurve animationCurve;

    public Transform spriteTransform;
    [Range(0,360)]
    public float rangeRotage;

    public override void Start()
    {
        curveAnimation = CurveAnimation.instance;
        animationCurve = curveAnimation.list_Anim_Curves[1].animCurve;
    }
    public override void FixedUpdate()
    {
        timeScale += Time.deltaTime;
        myBody.velocity = transform.right * speed * animationCurve.Evaluate(timeScale);
        angle += rangeRotage * animationCurve.Evaluate(timeScale);
        spriteTransform.eulerAngles = new Vector3(0, 0, angle);
        if (timeScale >= 3f)
        {
            Destroy(gameObject);
        }
    }
}
