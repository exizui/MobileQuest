using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud_KZ12 : Locations
{
    private QuestGiver questGiver;
    public GameObject pc;
    public override void Entry()
    {
        questGiver = GetComponent<QuestGiver>();
        base.Entry();
        OnEnter();
    }
    protected override void OnEnter()
    {
        DialogueTrigger.instance.TriggerDialogue(OnDialogueEnd);
    }
    public override void OnDialogueEnd()
    {
        questGiver.Give();

        pc.SetActive(true);

        QuestUI.instance.ShowExitDoor();
    }

    public override void Exit()
    {
        base.Exit();
    }

}
