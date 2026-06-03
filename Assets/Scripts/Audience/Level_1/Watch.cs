using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watch : Location 
{
    public GameObject key13;

    public override void Entry()
    {
        base.Entry();
        ALlowDialogue();
    }
    public void ALlowDialogue()
    {
        dialogueTrigger.TriggerDialogue(OnDialogueEnd);
    }

    public override void OnDialogueEnd()
    {
        key13.SetActive(true);
        QuestUI.instance.ShowExitDoor();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
