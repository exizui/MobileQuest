using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static QuestUI;

#region OLD QUEST
//public abstract class Quest : MonoBehaviour 
//{
//    public QuestID QuestID;


//    [SerializeField]
//    [TextArea]
//    private string description;

//    //[SerializeField][TextArea]
//    //private string[] stages;
//    protected Inventory inventory;
//    protected QuestUI questUI;
//    protected BaseLocations location;
//    public bool IsStarted { get; private set; }
//    public bool IsCompleted { get; private set; }
//    protected int currentIndex = 0; 
//    public virtual void Init(BaseLocations location)
//    {
//        this.location = location;

//        questUI = FindObjectOfType<QuestUI>();
//        inventory = FindObjectOfType<Inventory>();
//    }
//    public virtual void StartQuest()
//    {
//        //var audience = location.GetComponent<QuestAudience>();
//        //var audience = location.GetComponent<Audience>();
//        var holder = location.GetComponent<IQuestHolder>();
//        if (QuestManager.instance.IsCompleted(holder.HolderID, QuestID))
//            return;

//        if (IsStarted) return;

//        IsStarted = true;

//        UpdateHeader(GetFullDescription());
//        //currentIndex = 0;
//        //ShowCurrentStage();
//        OnStart();
//    }

//    protected abstract void OnStart();

//    protected void UpdateHeader(string header)
//    {
//        questUI.ShowHeader(header);
//    }
//    protected void UpdateUI(string progress) //раньше здесь было очищение стринга!!!
//    {
//        questUI.ShowDescription(progress);
//    }

//    protected virtual string GetFullDescription()
//    {
//        return description;
//    }
//    protected void Complete()
//    {
//        //var audience = location.GetComponent<QuestAudience>();
//        //var audience = location.GetComponent<Audience>();
//        var holder = location.GetComponent<IQuestHolder>();
//        if (holder == null)
//        {
//            Debug.LogError("Quest null!!!");
//        }

//        if (QuestManager.instance.IsCompleted(holder.HolderID, QuestID))
//            return;

//        QuestManager.instance.SetCompleted(holder.HolderID, QuestID);

//        IsCompleted = true;
//        Debug.Log("Квест выполнен" + IsCompleted);
//        EndQuest();
//        //
//    }
//    protected abstract void EndQuest();

//}
#endregion

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
                Debug.LogError("Custom step не найден: " + step.customStepID);
            }
        }
    }

    private void CompleteStep()
    {
        currentStepIndex++;
        //QuestManager.instance.SaveGameState(); ////////сейв на кожному кроці
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
        Debug.Log("Quest completed " + data.questID);

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

    private void OnLocationEntered(Locations location)
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
        Debug.Log("Получен триггер: " + triggerID);
        Debug.Log("Ожидается: " + CurrentStep.triggerID);

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
        if(data.rewards == null || data.rewards.Count == 0) return; 

        foreach (var reward in data.rewards)
        {
            if (reward != null)
            {
                reward.Give();
            }
        }
    }
    public void UpdateUI()
    {
        if(IsCompleted) return;
        if(QuestUI.instance == null) return;

        Debug.Log("Обнова шага + " + CurrentStep);
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

