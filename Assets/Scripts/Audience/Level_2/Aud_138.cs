using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud_138 : QuestAudience
{
    public override void Entry()
    {
        base.Entry();
        //Debug.Log("138");
        OnEnter();
    }

    protected override void OnEnter()
    {
        DialogueTrigger.instance.TriggerDialogue(OnDialogueEnd);
    }

    private void OnDialogueEnd()
    {
        QuestManager.singleton.OnLocationEntered(this);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
