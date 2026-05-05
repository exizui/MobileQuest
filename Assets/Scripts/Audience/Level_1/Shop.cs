using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Locations
{
    public InventorySecurity invSecurity;

    private void OnEnable()
    {
        InventorySecurity.instance.OnMessageActive += ActiveDialogue;
    }
    private void OnDisable()
    {
        InventorySecurity.instance.OnMessageActive -= ActiveDialogue;
    }
    public override void Entry()
    {
        base.Entry();

        invSecurity.EnterShop();

        dialogueTrigger.TriggerDialogue(OnDialogueEnd);
        
    }
    private void ActiveDialogue()
    {
        dialogueTrigger.repeatable = false;
    }

    public override void OnDialogueEnd()
    {
        QuestUI.instance.ShowExitDoor();
    }


    public override void Exit()
    {
        //GameState.instance.SetFlag("buyCoffeeDone");
        base.Exit();

    }
}
