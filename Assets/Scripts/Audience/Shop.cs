using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Locations
{

    public override void Entry()
    {
        base.Entry();

        dialogue.TriggerDialogue(ShowExit);
        
    }

    private void ShowExit()
    {
        ///////////////////////////////////////////
        QuestUI.instance.ShowExitDoor();

    }

    public override void Exit()
    {
        GameState.instance.SetFlag("buyCoffeeDone");
        base.Exit();

    }
}
