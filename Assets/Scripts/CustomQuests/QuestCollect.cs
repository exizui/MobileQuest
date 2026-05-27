using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestCollect : MonoBehaviour, IQuestHandler
{
    public string questID;
    private int progress;
    private Quest _quest; // посилання на теперешній квест
    public GameObject triggerObjectGroup;
    public string QuestID => questID;

    public void StartQuest(Quest quest)
    {

        _quest = quest;
        progress = 0;

        Debug.Log("Квест коллект почався");

        if (triggerObjectGroup != null)
            triggerObjectGroup.SetActive(true);
    }

    public void AddProgress()
    {
        progress++;

        if (progress >= 3)
        {
            Complete();
        }
    }

    public void Complete()
    {
        QuestUI.instance.ShowExitDoor();

        EventManager.instance.TriggerEvent("craft", 3);

        _quest.CompleteCurrentStep();

        triggerObjectGroup.SetActive(false);

        Notification.instance.ShowMessage("Тримай ключ від КЗ12 і частину ключа!", 4f);
    }
}