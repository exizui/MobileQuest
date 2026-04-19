using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud_138 : Locations
{
    public override void Entry()
    {
        base.Entry();
        //Debug.Log("138");
        OnEnter();
    }

    protected override void OnEnter()
    {
        dialogue.TriggerDialogue(OnDialogueEnd);
    }

    private void OnDialogueEnd()
    {
        //QuestManager.instance.OnLocationEntered(this);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
