using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud_147 : Locations
{

    private QuestGiver questGiver;


    public ItemData[] Coffee;

    public GameObject takeButton;

    public static event Action OnEntryShop;

    public override ILocationState GetState()
    {
        return new AudienceState();
    }

    private void Awake()
    {
        questGiver = GetComponent<QuestGiver>();
    }
    public override void Entry()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
        base.Entry();
        GetState();

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

    public override void OnDialogueEnd()
    {
        //QuestManager.instance.OnLocationEntered(this);
        questGiver.Give();
        //questUI.ActiveUI();
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
