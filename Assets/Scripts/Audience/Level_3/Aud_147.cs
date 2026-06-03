using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud_147 : Location
{
    public GameObject takeButton;

    public static event Action OnEntryShop;
    
    public override void Entry()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
        base.Entry();
  
        if (GameState.instance.HasFlag("takeDrink"))
        {
            takeButton.SetActive(true);
        }
        else
        {
            takeButton.SetActive(false);
        }

    }

    protected override void OnEnter()
    {
        dialogueTrigger.TriggerDialogue(OnDialogueEnd);
    }

    public override void OnDialogueEnd()
    {
        questGiver.Give();
        GameState.instance.SetFlag("buyCoffee");
        GameState.instance.SetFlag("questState");
        OnEntryShop?.Invoke();
        QuestUI.instance.ShowExitDoor();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
