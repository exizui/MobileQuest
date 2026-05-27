using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;

[Serializable]
public class SaveData
{
    public GameStateSaveData gameState;
    public InventorySaveData inventory;
    public QuestSaveData quests;
    public CraftSaveData craft;

    public StateLocation locationState;
    public int currentLocationID;
    public int prevLocationID;

    public bool isInventoryOpen;
    public bool isExitDoorOpen;

    public List<ObjectState> objectStates = new List<ObjectState>();
}

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;
    public const string QUEST_SAVE_KEY = "QuestProgress";
    public bool DeleteSave = false;

    public GameState gameState;
    public Inventory inventory;
    public QuestManager questManager;
    public InventoryUI inventoryUI;
    public SaveData CurrentData { get; private set; }
    private string folderPath => Application.persistentDataPath + "/save";
    //private string folderPath => "C:/MobileQuestSaves";
    private string filePath => folderPath + "/save.json";

    //public SaveData data = new SaveData();
    //private QuestManager progress;
    private HashSet<string> talked = new HashSet<string>();

    private void Awake()
    {
        if (instance == null) instance = this;

        if (DeleteSave) 
        {
            Debug.LogError("[SaveSystem] ВНИМАНИЕ! В инспекторе включена галочка 'DeleteSave'. Все сохранения удалены!");
            DeleteSaves();
        }
        Load();
    }
    private void Start()
    {
        //Load();
        var nav = LocationNavigator.Controller;

        if (nav != null && CurrentData != null)
        {
            nav.LoadLocation((LocationID)CurrentData.currentLocationID);
            nav.SetPrevLocation((LocationID)CurrentData.prevLocationID);
            //QuestUI.instance.ShowExitDoor();
            //nav.CheckState();
        }

        if (CurrentData != null && CurrentData.isInventoryOpen)
            inventoryUI.OpenInventory();

        if (CurrentData.isExitDoorOpen)
        {
            QuestUI.instance.ShowExitDoor();
        }
        else
        {
            print("Exit == null");
        }
           
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

        data.craft = (CraftSaveData)FindObjectOfType<CraftManager>().CaptureState();

        data.isInventoryOpen = inventoryUI.IsOpen();

        data.isExitDoorOpen = QuestUI.instance.IsExitDoorOpen;

        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        File.WriteAllText(filePath, JsonUtility.ToJson(data));

        //var saveables = FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>();
        var saveables = Resources.FindObjectsOfTypeAll<MonoBehaviour>().OfType<ISaveable>().Where(x => ((MonoBehaviour)x).gameObject.scene.isLoaded);

        #region OLD SAVEABLESSS
        //foreach (var saveable in saveables)
        //{
        //    //сейв кнопок 
        //    if (saveable is GameObjectSave buttonSave)
        //    {
        //        string key = "saveable_" + buttonSave.SaveID;
        //        bool value = (bool)buttonSave.CaptureState();
        //        PlayerPrefs.SetInt(key, value ? 1 : 0);
        //    }
        //    //if (saveable is GameObjectSave buttonSave)
        //    //{
        //    //    string key = "saveable_" + buttonSave.SaveID;
        //    //    bool[] states = (bool[])buttonSave.CaptureState();

        //    //    // зберігаємо кожен елемент окремо
        //    //    for (int i = 0; i < states.Length; i++)
        //    //        PlayerPrefs.SetInt(key + "_" + i, states[i] ? 1 : 0);

        //    //    PlayerPrefs.SetInt(key + "_count", states.Length); // кількість елементів
        //    //}
        //}
        #endregion[
        PlayerPrefs.Save();
        //
    }
    public void Load()
    {
        if(!File.Exists(filePath)) return;

        CurrentData = JsonUtility.FromJson<SaveData>(File.ReadAllText(filePath));

        if(CurrentData.gameState != null)
            gameState.RestoreState(CurrentData.gameState);

        if (CurrentData.inventory != null)
            inventory.RestoreState(CurrentData.inventory);

        if (CurrentData.quests != null) 
            questManager.RestoreState(CurrentData.quests);

        if (CurrentData.craft != null)
            FindObjectOfType<CraftManager>().RestoreState(CurrentData.craft);

        //nav.SetEnumState(CurrentData.locationState);
        //var saveables = FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>();
        var saveables = Resources.FindObjectsOfTypeAll<MonoBehaviour>().OfType<ISaveable>().Where(x => ((MonoBehaviour)x).gameObject.scene.isLoaded);


        #region OLD SAVEABLE
        //SINGLE
        //foreach (var saveable in saveables)
        //{
        //    if (saveable is GameObjectSave gameobjectSave)
        //    {
        //        string key = "saveable_" + gameobjectSave.SaveID;

        //        if (PlayerPrefs.HasKey(key))
        //        {
        //            bool value = PlayerPrefs.GetInt(key) == 1;
        //            gameobjectSave.RestoreState(value);
        //        }
        //    }
        //}

        //MASSIV
        //if (saveable is GameObjectSave gameobjectSave)
        //{
        //    string key = "saveable_" + gameobjectSave.SaveID;

        //    if (PlayerPrefs.HasKey(key + "_count"))
        //    {
        //        int count = PlayerPrefs.GetInt(key + "_count");
        //        bool[] states = new bool[count];

        //        for (int i = 0; i < count; i++)
        //            states[i] = PlayerPrefs.GetInt(key + "_" + i) == 1;

        //        gameobjectSave.RestoreState(states);
        //    }
        //}
        #endregion
    }


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

    public static int GetInt(string key, int defaultValue = 0)
    {
        return PlayerPrefs.GetInt(key, defaultValue);
    }

    public static void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }
    private void OnApplicationQuit()
    {
        Save();
    }
}
