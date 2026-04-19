using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestCollect : MonoBehaviour, IQuestHandler
{
    public string questID;
    private int progress;
    private Quest quest; // ссылка на текущий квест

    public GameObject triggerObjectGroup;
    //public ItemData itemKey;
    //public ItemData keyPart;
    public string QuestID => questID;
    public void StartQuest(Quest quest)
    {
        this.quest = quest;
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
        Debug.Log("Custom step завершен");


        Notification.instance.ShowMessage("Ви виконали квест і отримали ключ!");
        //Inventory.instance.AddItem(itemKey);
        QuestUI.instance.ShowExitDoor();
        EventManager.instance.TriggerEvent("craft", 3);
        //Inventory.instance.AddItem(keyPart);

        quest.CompleteCurrentStep();
    }
}