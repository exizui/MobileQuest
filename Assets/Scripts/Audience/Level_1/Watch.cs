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
    }

    public override void OnDialogueEnd()
    {
        key13.SetActive(true);
        QuestUI.instance.ShowExitDoor();
        //GameState.instance.DeleteFlag("tryOpenDoor");
    }

    public override void Exit()
    {
        base.Exit();
    }
}
