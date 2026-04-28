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
        dialogueTrigger.TriggerDialogue(ActiveKey);
        Debug.Log("АллоДиалог сработал");
    }

    private void ActiveKey()
    {
        key13.SetActive(true);
        QuestUI.instance.ShowExitDoor();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
