using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud_147 : Locations
{

    public QuestGiver questgiver;


    public ItemData[] Coffee;

    public GameObject takeButton;

    public static event Action OnEntryShop;

    public override ILocationState GetState()
    {
        return new AudienceState();
    }
    public override void Entry()
    {
        //dialogue = GetComponent<DialogueTrigger>();
        base.Entry();
        GetState();
        OnEnter();

        foreach (var item in Coffee)
        {
            if (Inventory.instance.HasItem(item))
            {
                takeButton.SetActive(true); 
                return;
            }
        }


    }

    protected override void OnEnter()
    {
        dialogueTrigger.TriggerDialogue(OnDialogueEnd);
    }

    private void OnDialogueEnd()
    {
        //QuestManager.instance.OnLocationEntered(this);
        questgiver.Give();
        questUI.ActiveUI();
        GameState.instance.SetFlag("buyCoffee");
        GameState.instance.SetFlag("questState");
        OnEntryShop?.Invoke();
        QuestUI.instance.ShowExitDoor();
    }

    public override void Exit()
    {
        base.Exit();
        //questUI.DisActiveUI();
    }
}
