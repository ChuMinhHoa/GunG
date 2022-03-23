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

    public Animator myanim;
    bool boom;
    public float inSensity;

    public override void Start()
    {
        curveAnimation = CurveAnimation.instance;
        animationCurve = curveAnimation.list_Anim_Curves[1].animCurve;
    }
    public override void OnInit(){}
    public override void FixedUpdate()
    {
        timeScale += Time.deltaTime;
        myBody.velocity = transform.right * speed * animationCurve.Evaluate(timeScale);
        angle += rangeRotage * animationCurve.Evaluate(timeScale);
        spriteTransform.eulerAngles = new Vector3(0, 0, angle);
        if (timeScale >= 3f && !boom)
        {
            boom = true;
            spriteTransform.eulerAngles = new Vector3(0, 0, 0);
            myanim.SetTrigger("Boom");
            CameraCinemachineController.instance.CameraShake(inSensity);
            Destroy(gameObject, .25f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        speed = 0;
    }
}
