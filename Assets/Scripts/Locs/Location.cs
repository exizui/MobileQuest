using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class Location : MonoBehaviour {

    [Header("Ідентифікатор локації")]
    public LocationID id;
    [Header("Стан локації")]
    public StateLocation stateType;

    [Header("Навігація")]
    public LocationID next;
    public LocationID prev;

    protected DialogueTrigger dialogueTrigger;
    protected QuestGiver questGiver;

    private void Awake()
    {
        if (dialogueTrigger == null)
            dialogueTrigger = GetComponent<DialogueTrigger>();
        if (questGiver == null)
            questGiver = GetComponent<QuestGiver>();
    }

    public virtual void Entry()
    {
        gameObject.SetActive(true);

        LocationEvents.OnLocationEntered?.Invoke(this);

        OnEnter();
    }
    protected virtual void OnEnter() { } 
    public virtual void OnDialogueEnd() { }

    public virtual void Exit()
    {
        gameObject.SetActive(false);
    }
}