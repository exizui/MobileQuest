using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud_125 : QuestAudience
{
    public override void Entry()
    {
        base.Entry();
        //TriggerObject();
        OnEnter();
    }

    protected override void OnEnter()
    {
        DialogueTrigger.instance.TriggerDialogue(OnDialogueEnd);
        OnDialogueEnd();
    }

    private void OnDialogueEnd()
    {
        QuestManager.singleton.OnLocationEntered(this);
        questUI.ActiveUI();
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("вышел");
        questUI.DisActiveUI();      
    }
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