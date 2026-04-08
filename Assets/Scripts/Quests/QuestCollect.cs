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

    public ItemData itemKey;
    private void Awake()
    {
        triggerObject = FindObjectOfType<TriggerObject>();   
    }

    protected override void OnStart()
    {
        Debug.Log("Квест коллект почався");
        //triggerObject.ActiveTrigger();
        triggerObjectGroup.SetActive(true);
    }

    public void AddProgress()
    {
        int_progress++;
        
        UpdateUI($"{int_progress}");

        if (int_progress == 5)
        {
            Complete();
        }
    }

    protected override void EndQuest()
    {
        //QuestUI.instance.ShowHeader();
        questUI.ShowHeader("Ви киконали завдання і отримали ключ від КЗ12");
        Inventory.instance.AddItem(itemKey);
        LocationNavigator.Controller.ShowExitDoor();
    }
}
