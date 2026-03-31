using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestProgress : MonoBehaviour
{
    public static QuestProgress Instance;

    private Dictionary<string, QuestState> states = new Dictionary<string, QuestState>();

    private const string SAVE_KEY = "QUEST_SAVE";

    [SerializeField] private bool isLoad = false;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadPref(); //
    }

    private void LoadPref()
    {
        if (isLoad)
        {
            Load();
        }
    }
   
    private string GetKey(AudienceID audience, QuestID quest)
    {
        return audience.ToString() + "_" + quest.ToString();
    }

    public void SetCompleted(AudienceID audience, QuestID quest)
    {
        //states[GetKey(audience,quest)] = QuestState.Completed;

        string key = GetKey(audience, quest);

        states[key] = QuestState.Completed;

        Save();

        Debug.Log($"Сохранено: {key}");
    }

    public bool IsCompleted(AudienceID audience, QuestID quest)
    {
        return states.TryGetValue(GetKey(audience, quest), out var state)
               && state == QuestState.Completed;

    }


    //private void OnEnable()
    //{
    //    DialogueTrigger.OnDialogueWas += OnDialogueStateChanged;
    //}

    //private void OnDisable()
    //{
    //    DialogueTrigger.OnDialogueWas -= OnDialogueStateChanged;
    //}

    //private void OnDialogueStateChanged(string key, bool isTalked)
    //{
    //    if (!isTalked) return;
    //    if (states.ContainsKey(key))
    //        states[key] = QuestState.Completed;
    //    else
    //        states.Add(key, QuestState.Completed);

    //    Save();
    //}
    private void Save()
    {
        SaveData data = new SaveData();

        foreach (var pair in states)
        {
            if (pair.Value == QuestState.Completed)
                data.completedKeys.Add(pair.Key);
        }

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
        PrintProgress();
        Debug.Log("Сохранено в плеер префс");
    }

    private void Load()
    {
        if (!PlayerPrefs.HasKey(SAVE_KEY))
            return;
        
        string json = PlayerPrefs.GetString(SAVE_KEY);

        SaveData data = JsonUtility.FromJson<SaveData>(json);

        states.Clear();

        foreach(var key in data.completedKeys)
        {
            states[key] = QuestState.Completed;
        }

        Debug.Log("Нагрузка из PLayerPrefs");
    }

    private void PrintProgress()
    {
        int completed = states.Count;
        int total = 2; 

        float percent = (float)completed / total * 100f;

        Debug.Log($"Прогресс: {percent}%");
    }

}
