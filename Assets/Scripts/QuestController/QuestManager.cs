using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public static class LocationEvents
{
    public static Action<Locations> OnLocationEntered; 
}
public class QuestManager : MonoBehaviour, ISaveable
{
    public static QuestManager instance;
    public Quest activeQuest;
    private List<Quest> activeQuests = new List<Quest>();
    private Dictionary<string, IQuestHandler> questHandlers = new Dictionary<string, IQuestHandler>();

    public event Action<ItemData> OnItemDelivered;
    public event Action<string> OnTriggerActivated;

    private Inventory inventory;

    public List<QuestData> allQuestDatabase;

    private List<string> completedQuestIDs = new List<string>();

    private List<string> rewardedQuestIDs = new List<string>();

    public bool IsRewarded(string questID) => rewardedQuestIDs.Contains(questID);

    public void MarkRewarded(string questID)
    {
        if (!rewardedQuestIDs.Contains(questID))
            rewardedQuestIDs.Add(questID);
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        inventory = FindObjectOfType<Inventory>();

        var handlers = GetComponentsInChildren<MonoBehaviour>(true);

        foreach (var h in handlers)
        {
            if (h is IQuestHandler handler)
            {
                questHandlers[handler.QuestID] = handler;
                Debug.Log("Quest Register " + handler.QuestID);
            }
        }
        //DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        StartCoroutine(LoadRoutine());
    }

    private IEnumerator LoadRoutine()
    {
        Debug.Log("Починаю завантаження...");

        yield return null; // чекаємо Awake всіх об'ектів

        if (SaveSystem.instance == null)
        {
            Debug.LogError("SaveSystem == null!");
            yield break;
        }

        //загружаємо сейви 
        //SaveSystem.instance.Load();

        //беремо дані квестів з SaveSystem
        var data = SaveSystem.instance.CurrentData; 

        if (data == null || data.quests == null)
        {
            Debug.Log("Нема данних квестів");
            yield break;
        }

        //викликаємо відновлення стану квестів
        RestoreState(data.quests);
        Debug.Log("Active quests after load: " + activeQuests.Count);
        //чекаємо ще один кадр 
        yield return null;

        if (activeQuest != null)
        {
            if (QuestUI.instance != null)
            {
                activeQuest.UpdateUI();
                Debug.Log("UI відновлений");
            }
            else
            {
                Debug.LogError("QuestUI всё ещё null");
            }
        }
    }
    public void CompleteQuest(Quest quest)
    {
        activeQuests.Remove(quest);

        if (!completedQuestIDs.Contains(quest.data.questID)) completedQuestIDs.Add(quest.data.questID);
        //prevQuest = activeQuest;
        if (activeQuest == quest) activeQuest = null;

        //SaveGameState(); ///

        //Save();
    }


    public void AddQuest(Quest quest)
    {
        activeQuests.Add(quest);

        if (activeQuest == null)
            activeQuest = quest;

        Debug.Log("Квест добавлен: " + quest.data.questID);
    }
    public void StartQuest(QuestData data)
    {
        if (IsQuestActive(data.questID))
            return;

        var quest = new Quest(data, inventory);

        Debug.Log("Квест запущен: " + data.questID);
        AddQuest(quest);

        //SaveGameState();
        //OnQuestListChanged?.Invoke();

        //Save();
    }

    public bool CanEnter(LocationID room)
    {
        var activeQuest = GetActivePriorityQuest();

        if (activeQuest == null) 
            return true;

        if(activeQuest.data.allowedRooms == null ||
            activeQuest.data.allowedRooms.Count == 0)
            return true;

        return activeQuest.data.allowedRooms.Contains(room);
    }

    public Quest GetActivePriorityQuest()
    {
        return activeQuest; // один обраний квест
    }
    ///
    public int GetCompletedCount()
    {
        return completedQuestIDs.Count; 
    }
    ///
    public IQuestHandler GetQuestHandler(string id)
    {
        return questHandlers.TryGetValue(id, out var handler) ? handler : null;
    }
    public bool IsQuestActive(string id)
    {
        return activeQuests.Exists(q => q.data.questID == id && !q.IsCompleted);
    }

    public void ItemDelivered(ItemData item)
    {
        OnItemDelivered?.Invoke(item);   
        Inventory.instance.RemoveItem(item); //////////!!!!!!
    }

    public void Trigger(string targetID)
    {
        OnTriggerActivated?.Invoke(targetID);
    }

    public object CaptureState()
    {
        QuestSaveData data = new QuestSaveData();

        foreach (var q in activeQuests)
        {
            data.activeQuests.Add(new QuestProgressData
            {
                questID = q.data.questID,
                currentStep = q.GetCurrentStepIndex()
            });
        }

        data.completedQuests = new List<string>(completedQuestIDs);
        data.rewardedQuests = new List<string>(rewardedQuestIDs); //
        
        return data;
    }

    public void RestoreState(object state)
    {
        QuestSaveData data = state as QuestSaveData;
        if(data == null) return;

        activeQuests.Clear();
        completedQuestIDs = new List<string>(data.completedQuests);
        rewardedQuestIDs = new List<string>(data.rewardedQuests); //

        foreach (var savedQuest in data.activeQuests)
        {
            var questAsset = allQuestDatabase.Find(x => x.questID == savedQuest.questID);

            if (questAsset != null)
            {
                Quest loadedQuest = new Quest(questAsset, inventory, savedQuest.currentStep);
                AddQuest(loadedQuest);
            }
        }

        if(activeQuests.Count > 0)
        {
            activeQuest = activeQuests[0];
            activeQuest.UpdateUI();
        }
    }

    //private void Save()
    //{
    //    SaveSystem.instance.Save();
    //}
    //private void OnApplicationQuit()
    //{
    //    Save();
    //}
}

