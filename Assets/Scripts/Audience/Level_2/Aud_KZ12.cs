using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud_KZ12 : QuestAudience
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
        //LocationNavigator.Controller.ShowExitDoor();
    }

    public override void Exit()
    {
        base.Exit();
        //questUI.DisActiveUI();

    }

}
