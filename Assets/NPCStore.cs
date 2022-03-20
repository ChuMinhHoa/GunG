using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStore : NPC
{
    public GameObject[] storePanel;
    bool activeBag = false;
    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.I))
        {
            activeBag = !activeBag;
            storePanel[0].SetActive(activeBag);
        }
    }

    public override void OnTrigger()
    {
        base.OnTrigger();
        foreach (GameObject gameObject in storePanel)
        {
            gameObject.SetActive(true);
        }
    }
    public override void OnExit()
    {
        base.OnExit();
    }
    public void ExitStorePanel() {
        foreach (GameObject gameObject in storePanel)
        {
            gameObject.SetActive(false);
        }
    }
}
