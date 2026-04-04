using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData
{
    public List<string> completedKeys = new List<string>();
}

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;

    private string path;
    public SaveData data = new SaveData();

    private QuestProgress progress;
    private HashSet<string> talked = new HashSet<string>();

    public bool DeleteSave = false;
    private void Awake()
    {
        progress = FindObjectOfType<QuestProgress>();
        if (instance == null)
            instance = this;

        //path = Application.persistentDataPath + "/save.json"; //для мобилки 
        path = Application.dataPath + "/Saves/save.json";
        //Load();
        if (DeleteSave)
        {
            ResetAll();
        }
    }


    private void OnEnable()
    {
        //DialogueTrigger.OnSave += 
    }

    private void OnDisable()
    {
        //DialogueTrigger.OnSave -= 
    }

    public static bool IsTalked(string id)
    {
        return PlayerPrefs.GetInt(id, 0) == 1;
    }

    public static void SetTalked(string id)
    {
        PlayerPrefs.SetInt(id, 1);
        PlayerPrefs.Save();
    }
    public static void ResetAll()
    {
        PlayerPrefs.DeleteAll();
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
