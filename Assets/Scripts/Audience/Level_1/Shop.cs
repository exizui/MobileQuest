using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Locations
{
    public ItemData trueCoffee; 
    public override void Entry()
    {
        base.Entry();

        dialogueTrigger.TriggerDialogue(ShowExit);
        
    }

    private void ShowExit()
    {
        ///////////////////////////////////////////
        QuestUI.instance.ShowExitDoor();

    }
    private void Check()
    {
        bool isTrueCoffee = Inventory.instance.HasItem(trueCoffee);
        if (isTrueCoffee)
        {
            GameState.instance.DeleteFlag("buyCoffee");
        }
    }
    public override void Exit()
    {
        //GameState.instance.SetFlag("buyCoffeeDone");
        Check();
        base.Exit();

    }
}
