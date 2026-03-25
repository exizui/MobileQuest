using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud_125 : Audience
{
    private DialogueManager dialogueManager;
    public override void Entry()
    {
        base.Entry();
        //Debug.Log("125");
        //QuestManager.singleton.OnLocationEntered(this);
        //questUI.ActiveUI();
        TriggerObject();
    }

    public void TriggerObject()
    {
        DialogueTrigger.instance.TriggerDialogue(ActiveQuest);
    }
    public void ActiveQuest()
    {
        QuestManager.singleton.OnLocationEntered(this);
        questUI.ActiveUI();
    }
    public override void Exit()
    {
        base.Exit();
        questUI.DisActiveUI();
        
    }
}
