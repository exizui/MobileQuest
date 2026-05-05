using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watch : Locations 
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
        Debug.Log("АллоДиалог сработал");
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
