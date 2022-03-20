using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Canvas : MonoBehaviour
{
    public UIName uIName;

    public virtual void Start() {
        OnInit();
    }
    public virtual void OnInit() { }
    public virtual void OnOpen() { }
    public virtual void OnClose() { }
}
