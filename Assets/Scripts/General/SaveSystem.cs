using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

[Serializable]
public class SaveData
{
    public GameStateSaveData gameState;
    public InventorySaveData inventory;
    public QuestSaveData quests;
    public CraftSaveData craft;
    public EventManagerSaveData events;

    public StateLocation locationState;
    public int currentLocationID;
    public int prevLocationID;

    public bool isInventoryOpen;
    public bool isExitDoorOpen;

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
    public EventManager eventManager;
    public SaveData CurrentData { get; private set; }
    private string folderPath => Application.persistentDataPath + "/save";
    private string filePath => folderPath + "/save.json";

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
        var nav = LocationNavigator.Controller;

        if (nav != null && CurrentData != null)
        {
            nav.LoadLocation((LocationID)CurrentData.currentLocationID);
            nav.SetPrevLocation((LocationID)CurrentData.prevLocationID);
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

        data.currentLocationID = (int)nav.CurrentLocationID();

        data.prevLocationID = (int)nav.PrevLocationID();

        data.craft = (CraftSaveData)FindObjectOfType<CraftManager>().CaptureState();

        data.isInventoryOpen = inventoryUI.IsOpen();

        data.isExitDoorOpen = QuestUI.instance.IsExitDoorOpen;

        data.events = (EventManagerSaveData)eventManager.CaptureState();

        Directory.CreateDirectory(Path.GetDirectoryName(filePath));
        File.WriteAllText(filePath, JsonUtility.ToJson(data));

        var saveables = Resources.FindObjectsOfTypeAll<MonoBehaviour>().OfType<ISaveable>().Where(x => ((MonoBehaviour)x).gameObject.scene.isLoaded);


        PlayerPrefs.Save();
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

        if (CurrentData.events != null)
            eventManager.RestoreState(CurrentData.events);
;
        var saveables = Resources.FindObjectsOfTypeAll<MonoBehaviour>().
            OfType<ISaveable>().Where(x => ((MonoBehaviour)x).gameObject.scene.isLoaded);
    }


    public static bool IsTalked(string id) => PlayerPrefs.GetInt(id, 0) == 1;

    public static void SetTalked(string id)
    {
        PlayerPrefs.SetInt(id, 1);
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
