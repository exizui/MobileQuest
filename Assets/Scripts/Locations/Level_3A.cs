using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_3A : Locations
{

    public GameObject _gameObject;
    public ItemData pc;

    public override void Entry()
    {
        base.Entry();

        if (Inventory.instance.HasItem(pc))
        {
            AddLogic();
        }
    }

    private void AddLogic()
    {
        _gameObject.AddComponent<TriggerDoor>();
        print("ADDLOGIC");

    }

    public override void Exit()
    {
        base.Exit();
    }
}
