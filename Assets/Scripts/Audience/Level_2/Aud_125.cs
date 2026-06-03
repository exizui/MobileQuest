using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud_125 : Location
{
    public override void Entry()
    {
        base.Entry();
        LocationEvents.OnLocationEntered?.Invoke(this);
    }

    protected override void OnEnter()
    {
        dialogueTrigger.TriggerDialogue(OnDialogueEnd);
    }

    public override void OnDialogueEnd()
    {
        questGiver.Give();
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("вышел");
    }

}

