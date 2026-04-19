using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud_KZ12 : Locations
{
    private QuestGiver questGiver;
    public override void Entry()
    {
        questGiver = GetComponent<QuestGiver>();
        base.Entry();
        //TriggerObject();
        OnEnter();
    }
    protected override void OnEnter()
    {
        DialogueTrigger.instance.TriggerDialogue(OnDialogueEnd);
    }
    private void OnDialogueEnd()
    {
        //QuestManager.instance.OnLocationEntered(this);
        questUI.ActiveUI();
        questGiver.Give();

        QuestUI.instance.ShowExitDoor();
    }

    public override void Exit()
    {
        base.Exit();
        //questUI.DisActiveUI();

    }

}
