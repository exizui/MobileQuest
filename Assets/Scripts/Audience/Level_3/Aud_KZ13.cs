using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud_KZ13 : Location
{
    public GameObject givePC;
    public GameObject minigame;

    private string TriggerID = "backMiniGame";
    public override void Entry()
    {
        base.Entry();
    }
    protected override void OnEnter()
    {
        if (GameState.instance.HasFlag("tryOpenDoor"))
        {
            givePC.SetActive(true);
        }
        else
        {
            minigame.SetActive(true);
        }

        if (GameState.instance.HasFlag("AllowBack"))
        {
            minigame.SetActive(false);
            givePC.SetActive(false);
            dialogueTrigger.TriggerDialogue(OnDialogueEnd);
        }
    }
    public override void OnDialogueEnd()
    {
        QuestUI.instance.ShowExitDoor();
        QuestManager.instance.Trigger(TriggerID);
    }

    public override void Exit()
    {
        base.Exit();
    }

}