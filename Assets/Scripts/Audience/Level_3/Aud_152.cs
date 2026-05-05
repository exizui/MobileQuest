using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud_152 : Locations
{
    private QuestGiver questGiver;

    private void Awake()
    {
        questGiver = GetComponent<QuestGiver>();
    }
    public override void Entry()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
        base.Entry();
       
        OnEnter();
    }

    protected override void OnEnter()
    {
        dialogueTrigger.TriggerDialogue(OnDialogueEnd);
    }
    public override void OnDialogueEnd()
    {
        questGiver.Give();
        //questUI.ActiveUI();
    }
    public override void Exit()
    {
        base.Exit();
    }
}
