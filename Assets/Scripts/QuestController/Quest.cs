using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Quest
{
    public QuestData data;
    public int GetCurrentStepIndex() => currentStepIndex;
    private int currentStepIndex = 0;
    public bool IsCompleted { get; private set; }

    private Inventory inventory;

    private List<bool> stepCompleted = new List<bool>();

    private int completedQuest { get; set; } = 0; ///
    public Quest(QuestData data, Inventory inventory, int startIndex = 0) ///startIndex
    {
        this.data = data;
        this.inventory = inventory;
        this.currentStepIndex = startIndex; ///

        inventory.OnItemAdded += OnItemAdded;
        LocationEvents.OnLocationEntered += OnLocationEntered;
        QuestManager.instance.OnItemDelivered += OnItemDelivered;
        QuestManager.instance.OnTriggerActivated += OnTriggerActivated;

        StartStep();
    }

    private QuestStepData CurrentStep => data.steps[currentStepIndex];

    private void StartStep()
    {
        var step = CurrentStep;

        Debug.Log("Step: " + step.description);

        UpdateUI();

        if (step.stepType == QuestStepType.Custom)
        {
            var handler = QuestManager.instance.GetQuestHandler(step.customStepID);

            if (handler != null)
            {
                handler.StartQuest(this);
            }
            else
            {
                Debug.LogError("Custom step не знайдений: " + step.customStepID);
            }
        }
    }

    private void CompleteStep()
    {
        currentStepIndex++;
        //QuestManager.instance.SaveGameState(); //сейв на кожному кроці
        SaveSystem.instance.Save();
        if (currentStepIndex >= data.steps.Length)
        {
            CompleteQuest();
            return;
        }

        StartStep();
    }
    private void CompleteQuest()
    {
        IsCompleted = true;
        Debug.Log("Квест виконано " + data.questID);

        GiveRewards();
        RemoveEvents();

        QuestUI.instance.CompleteQuest("Квест виконано!");
        QuestManager.instance.CompleteQuest(this);////осторожно мб буду менять

        completedQuest++;
        Debug.Log("Виконано квестів " + completedQuest);
    }

    private void OnItemAdded(ItemData item)
    {
        if (IsCompleted) return;

        var step = CurrentStep;

        if (step.stepType == QuestStepType.CollectItem && 
            step.item == item)
        {
            CompleteStep();
        }
    }

    private void OnLocationEntered(Location location)
    {
        if(IsCompleted) return;

        var step = CurrentStep;

        if(step.stepType == QuestStepType.GoToLocation &&
            step.locationID == location.id)
        {
            CompleteStep();
        }
    }


    private void OnItemDelivered(ItemData item)
    {
        if (IsCompleted) return;

        var step = CurrentStep;

        if (step.stepType == QuestStepType.DeliverItem &&
            item == CurrentStep.item)
        {
            CompleteStep();
        }
    }

    private void OnTriggerActivated(string triggerID)
    {
        Debug.Log("Отриманий трігер: " + triggerID);
        Debug.Log("Очікується: " + CurrentStep.triggerID);

        if (IsCompleted) return;

        var step = CurrentStep;

        if (step.stepType == QuestStepType.Trigger &&
           CurrentStep.triggerID == triggerID)
        {
            CompleteStep();
        }
    }

    public void CompleteCurrentStep()
    {
        CompleteStep();
    }

    private void GiveRewards()
    {
        if (data.rewards == null || data.rewards.Count == 0) return; //

        if (QuestManager.instance.IsRewarded(data.questID)) return;//

        QuestManager.instance.MarkRewarded(data.questID);//

        foreach (var reward in data.rewards)
        {
            if (reward != null)   
                reward.Give();
        }
    }
    public void UpdateUI()
    {
        if(IsCompleted) return;
        if(QuestUI.instance == null) return;

        Debug.Log("Оновлення кроку+ " + CurrentStep);
        QuestUI.instance.ShowHeader(CurrentStep.description);
        QuestUI.instance.ActiveUI();
    }

    private void RemoveEvents()
    {
        inventory.OnItemAdded -= OnItemAdded;
        LocationEvents.OnLocationEntered -= OnLocationEntered;
        QuestManager.instance.OnItemDelivered -= OnItemDelivered;
        QuestManager.instance.OnTriggerActivated -= OnTriggerActivated;
    }
}

