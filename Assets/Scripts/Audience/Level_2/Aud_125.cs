using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud_125 : Locations
{
    private QuestGiver questGiver;

    public override void Entry()
    {
        questGiver = GetComponent<QuestGiver>();
        base.Entry();
        LocationEvents.OnLocationEntered?.Invoke(this);
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
        Debug.Log("вышел");
        //questUI.DisActiveUI();
    }

    //protected override void OnEnter()
    //{
    //    // ❗ НЕ вызываем base.OnEnter(), чтобы перехватить логику
    //    if (dialogue != null)
    //    {
    //        dialogue.TriggerDialogue(OnDialogueEnd);
    //    }
    //    else
    //    {
    //        StartQuestAfterDialogue();
    //    }
    //}

    //private void OnDialogueEnd()
    //{
    //    StartQuestFlow();
    //}

    //private void StartQuestAfterDialogue()
    //{
    //    QuestManager.instance.OnLocationEntered(this);
    //    questUI.ActiveUI();
    //}

    //protected override void OnExit()
    //{
    //    base.OnExit();
    //}
}

#region OldQuestTrigger

//public void TriggerObject()
//{
//    DialogueTrigger.instance.TriggerDialogue(ActiveQuest);
//}
//public void ActiveQuest()
//{
//    QuestManager.singleton.OnLocationEntered(this);
//    questUI.ActiveUI();
//}
#endregion