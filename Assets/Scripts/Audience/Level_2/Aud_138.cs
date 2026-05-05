using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud_138 : Locations
{
    private QuestGiver questGiver;
    [Header("Кнопка квесту")]
    [SerializeField] private GameObject questButton;
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
        questButton.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
