using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveAnimation : MonoBehaviour
{
    public static CurveAnimation instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else instance = this;
    }
    public List<AnimationCurveData> list_Anim_Curves; 
}
