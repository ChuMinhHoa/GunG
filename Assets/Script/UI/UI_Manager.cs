using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIName {UIBar, UIStore, UIBag}
public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else instance = this;
    }

    public List<UI_Canvas> list_UICanvases;

    public void AddUICanvas(UI_Canvas uI_Canvas) {
        list_UICanvases.Add(uI_Canvas);
    }
}
