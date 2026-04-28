using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

#region OLD QUEST MANAGER 
//public class QuestManager : MonoBehaviour
//{
//    public static QuestManager instance;
//    public Dictionary<string, QuestState> states = new Dictionary<string, QuestState>();

//    public Slider progressBar;
//    private void Awake()
//    {
//        instance = this;
//    }

//    public bool OnLocationEntered(BaseLocations location)
//    {
//        //var questHolder = location.GetComponent<QuestAudience>();
//        //var questHolder = location.GetComponent<Audience>();
//        var questHolder = location.GetComponent<IQuestHolder>();

//        if (questHolder == null)
//            return false;

//        foreach (var questID in questHolder.Quests)
//        {
//            if (!IsCompleted(questHolder.HolderID, questID))
//            {
//                StartQuest(location, questID);
//                return true;
//            }
//        }

//        return false;
//    }

//    public void StartQuest(BaseLocations location, QuestID questID)
//    {
//        var quests = location.GetComponents<Quest>();

//        foreach (var quest in quests)
//        {
//            if (quest.QuestID == questID)
//            {
//                quest.Init(location);
//                quest.StartQuest();
//                break;
//            }
//        }
//    }

//    private string GetKey(string holderID, QuestID quest)
//    {
//        return holderID + "_" + quest.ToString();
//    }

//    public void SetCompleted(string holderID, QuestID quest)
//    {

//        string key = GetKey(holderID, quest);

//        states[key] = QuestState.Completed;

//        //PrintProgress();

//        //Debug.Log($"Сохранено: {key}");
//    }

//    public bool IsCompleted(string holderID, QuestID quest)
//    {
//        return states.TryGetValue(GetKey(holderID, quest), out var state)
//               && state == QuestState.Completed;

//    }
//    private void PrintProgress()
//    {
//        int completed = states.Count;
//        int total = 3;

//        float percent = (float)completed / total * 100f;
//        progressBar.value = percent;
//        Debug.Log($"Прогресс: {percent}%");
//    }
//}
#endregion

public static class LocationEvents
{
    public static Action<Locations> OnLocationEntered; ///////////////
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

    private void Awake()
    {
        //if (instance != null && instance != this)
        //{
        //    Destroy(gameObject);
        //    return;
        //}

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
    //public void RegisterHandler(IQuestHandler handler)
    //{
    //    questHandlers[handler.QuestID] = handler;
    //}
    //private IEnumerator LoadRoutine()
    //{
    //    Debug.Log("Начинаю загрузку...");

    //    // Ждем, пока все Awake завершатся
    //    yield return null;

    //    if (SaveSystem.instance == null)
    //    {
    //        Debug.LogError("SaveSystem не найден на сцене!");
    //        yield break;
    //    }

    //    var data = SaveSystem.instance.LoadQuests();
    //    if (data == null)
    //    {
    //        Debug.Log("Файл сохранения пуст или отсутствует.");
    //        yield break;
    //    }

    //    completedQuestIDs = data.completedQuests;

    //    foreach (var savedQuest in data.activeQuests)
    //    {
    //        var questAsset = allQuestDatabase.Find(x => x.questID == savedQuest.questID);
    //        if (questAsset != null)
    //        {
    //            // Передаем индекс шага в конструктор
    //            Quest loadedQuest = new Quest(questAsset, inventory, savedQuest.currentStep);
    //            activeQuests.Add(loadedQuest);

    //            if (activeQuest == null)
    //                activeQuest = loadedQuest;
    //        }
    //        else
    //        {
    //            Debug.LogWarning($"Квест с ID {savedQuest.questID} не найден в базе данных!");
    //        }
    //    }

    //    // Финальная проверка UI
    //    if (activeQuest != null)
    //    {
    //        if (QuestUI.instance != null)
    //        {
    //            int stepIdx = activeQuest.GetCurrentStepIndex();
    //            string desc = activeQuest.data.steps[stepIdx].description;

    //            QuestUI.instance.ShowHeader(desc);
    //            Debug.Log("UI успешно восстановлен: " + desc);
    //        }
    //        else
    //        {
    //            Debug.LogError("QuestUI.instance всё еще null!");
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("Нет активных квестов для отображения.");
    //    }
    //}


    //public void SaveGameState()
    //{
    //    SaveSystem.instance.SaveQuests(activeQuests, completedQuestIDs);
    //}


    private IEnumerator LoadRoutine()
    {
        Debug.Log("Начинаю загрузку...");

        yield return null; // ждём Awake всех объектов

        if (SaveSystem.instance == null)
        {
            Debug.LogError("SaveSystem не найден!");
            yield break;
        }

        // 👉 грузим весь сейв
        SaveSystem.instance.Load();

        // 👉 берём данные квестов из SaveSystem
        var data = SaveSystem.instance.CurrentData; // 👈 см. ниже

        if (data == null || data.quests == null)
        {
            Debug.Log("Нет данных квестов");
            yield break;
        }

        // 👉 используем ТВОЙ RestoreState
        RestoreState(data.quests);
        Debug.Log("Active quests after load: " + activeQuests.Count);
        // 👉 ВАЖНО: ещё один кадр ждём UI
        yield return null;

        if (activeQuest != null)
        {
            if (QuestUI.instance != null)
            {
                activeQuest.UpdateUI();
                Debug.Log("UI восстановлен");
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

        if (activeQuest == quest) activeQuest = null;

        //SaveGameState(); ///
        Save();
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
        Save();
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
        return activeQuest; // один выбранный
    }



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

        return data;
    }

    public void RestoreState(object state)
    {
        QuestSaveData data = state as QuestSaveData;
        if(data == null) return;

        activeQuests.Clear();
        completedQuestIDs = new List<string>(data.completedQuests);

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

    private void Save()
    {
        SaveSystem.instance.Save();
    }
    private void OnApplicationQuit()
    {
        Save();
    }
    //public void NotifyQuestUpdated()
    //{
    //    OnQuestListChanged?.Invoke();
    //}
}

