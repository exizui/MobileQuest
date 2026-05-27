using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud_125 : Locations
{
    private QuestGiver questGiver;

    public override void Entry()
    {
        questGiver = GetComponent<QuestGiver>();
        base.Entry();
        LocationEvents.OnLocationEntered?.Invoke(this);
        OnEnter();
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

