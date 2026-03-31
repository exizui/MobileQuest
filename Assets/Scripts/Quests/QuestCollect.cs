using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class QuestCollect : Quest
{
    private int int_progress;
    private TriggerObject triggerObject;
    public GameObject triggerObjectGroup;
    private void Awake()
    {
        triggerObject = FindObjectOfType<TriggerObject>();
    }

    protected override void OnStart()
    {
        Debug.Log("Квест коллект почався");
        triggerObjectGroup.SetActive(true);
        //triggerObject.ActiveTrigger();
        triggerObjectGroup.SetActive(true);
    }

    public void AddProgress()
    {
        int_progress++;
        
        UpdateUI($"{int_progress}");

        if (int_progress == 6)
        {
            Complete();
            EndQuest();
        }
    }

    protected override void EndQuest()
    {
        //QuestUI.instance.ShowHeader();
        questUI.ShowHeader("Квест зроблений!!! Молодець!!!");
        LocationNavigator.Controller.ShowExitDoor();
    }
}
