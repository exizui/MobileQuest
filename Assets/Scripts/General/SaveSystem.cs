using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;


public interface ISaveable
{
    object CaptureState();
    void RestoreState(object state);
}

[Serializable]
public class SaveData
{
    public GameStateSaveData gameState;
    public InventorySaveData inventory;
    public QuestSaveData quests;

    public StateLocation locationState;
    public int currentLocationID;
    public int prevLocationID;
}

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;
    public const string QUEST_SAVE_KEY = "QuestProgress";
    public bool DeleteSave = false;

    public GameState gameState;
    public Inventory inventory;
    public QuestManager questManager;
    public SaveData CurrentData { get; private set; }
    private string folderPath => Application.persistentDataPath + "/save";
    private string filePath => folderPath + "/save.json";

    //public SaveData data = new SaveData();
    //private QuestManager progress;
    private HashSet<string> talked = new HashSet<string>();

    private void Awake()
    {
        if (instance == null) instance = this;

        if (DeleteSave)
        {
            DeleteSaves();
        }
    }
    private void Start()
    {
        Load();
    }
    public void Save()
    {
        SaveData data = new SaveData();

        data.gameState = (GameStateSaveData)gameState.CaptureState();
        data.inventory = (InventorySaveData)inventory.CaptureState();
        data.quests = (QuestSaveData)questManager.CaptureState();

        var nav = LocationNavigator.Controller;
        //data.locationState = nav.currentStateType;
        data.currentLocationID = (int)nav.CurrentLocationID();
        data.prevLocationID = (int)nav.PrevLocationID();

        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        File.WriteAllText(filePath, JsonUtility.ToJson(data));
    }
    public void Load()
    {
        if(!File.Exists(filePath)) return;

        CurrentData = JsonUtility.FromJson<SaveData>(File.ReadAllText(filePath));

        if(CurrentData.gameState != null)
            gameState.RestoreState(CurrentData.gameState);

        if(CurrentData.inventory != null)
            inventory.RestoreState(CurrentData.inventory);

        if(CurrentData.quests != null) 
            questManager.RestoreState(CurrentData.quests);

        var nav = LocationNavigator.Controller;
        nav.LoadLocation((LocationID)CurrentData.currentLocationID);
        //nav.SetEnumState(CurrentData.locationState);

        nav.SetPrevLocation((LocationID)CurrentData.prevLocationID);   
    }


    //public void SaveQuests(List<Quest> activeQuests, List<string> completedQuests)
    //{
    //    SaveData saveData = new SaveData();

    //    foreach (var q in activeQuests)
    //    {
    //        saveData.activeQuests.Add(new QuestProgressData
    //        {
    //            questID = q.data.questID,
    //            currentStep = q.GetCurrentStepIndex()
    //        });
    //        Debug.Log("Сохраняю квестов: " + activeQuests.Count);
    //    }

    //    saveData.completedQuests = completedQuests;

    //    string json = JsonUtility.ToJson(saveData);
    //    PlayerPrefs.SetString(QUEST_SAVE_KEY, json);
    //    PlayerPrefs.Save();
    //    Debug.Log("Квесты сохранены");
    //}

    //public SaveData LoadQuests()
    //{
    //    if(!PlayerPrefs.HasKey(QUEST_SAVE_KEY)) return null;

    //    string json = PlayerPrefs.GetString(QUEST_SAVE_KEY);
    //    return JsonUtility.FromJson<SaveData>(json);
    //}
    public static bool IsTalked(string id) => PlayerPrefs.GetInt(id, 0) == 1;

    public static void SetTalked(string id)
    {
        PlayerPrefs.SetInt(id, 1);
        PlayerPrefs.Save();
    }

    public void SaveLocation(string key, int locID)
    {

        PlayerPrefs.SetInt(key, (int)locID);



        PlayerPrefs.Save();
    }
    public void DeleteSaves()
    {
        Debug.Log(filePath);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("DELETE SAVES");
        }
        PlayerPrefs.DeleteAll();
    }



    private void OnApplicationQuit()
    {
        Save();
    }

    //private void SaveDialogue(string id, bool isTalked)
    //{
    //    //if (!progress.states.ContainsKey(id))
    //    //    progress.states.Add(id,QuestState.Completed);
    //    if (!isTalked) return;

    //    talked.Add(id);
    //}
    //public bool IsTalked(string id)
    //{
    //    return talked.Contains(id);
    //}

    //private void Save()
    //{
    //    SaveData data = new SaveData();

    //    foreach (var pair in progress.states)
    //    {
    //        if(pair.Value == QuestState.Completed)
    //           data.completedKeys.Add(pair.Key);
    //    }

    //    string json = JsonUtility.ToJson(data, true);
    //    File.WriteAllText(path, json);

    //    Debug.Log("Сохранено: " + path);
    //}
    //public void Load()
    //{
    //    if (!File.Exists(path))
    //    {
    //        return;
    //    }

    //    string json = File.ReadAllText(path);
    //    SaveData data = JsonUtility.FromJson<SaveData>(json);
    //    progress.states = new Dictionary<string, QuestState>();

    //    foreach (var key in data.completedKeys)
    //    {
    //        progress.states[key] = QuestState.Completed;
    //    }
    //    Debug.Log("zahrusheno");
    //}
}
