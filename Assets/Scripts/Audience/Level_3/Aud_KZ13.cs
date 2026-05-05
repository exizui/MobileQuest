using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud_KZ13 : Locations
{
    private string TriggerID = "backMiniGame";
    public override void Entry()
    {
        base.Entry();

        if (GameState.instance.HasFlag("AllowBack"))
        {
            dialogueTrigger.TriggerDialogue(OnDialogueEnd);
        }
    }

    public override void OnDialogueEnd()
    {
        QuestManager.instance.Trigger(TriggerID);
    }

    public override void Exit()
    {
        base.Exit();

    }


}