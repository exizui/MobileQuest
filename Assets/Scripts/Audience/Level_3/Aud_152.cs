using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud_152 : Locations
{
    public override void Entry()
    {
        base.Entry();
        Debug.Log("152");
       
        OnEnter();
    }

    protected override void OnEnter()
    {
        QuestUI.instance.ShowExitDoor();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
