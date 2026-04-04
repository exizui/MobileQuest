using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestProgress : MonoBehaviour
{
    public static QuestProgress Instance;

    public Dictionary<string, QuestState> states = new Dictionary<string, QuestState>();

    public Slider progressBar;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private string GetKey(AudienceID audience, QuestID quest)
    {
        return audience.ToString() + "_" + quest.ToString();
    }

    public void SetCompleted(AudienceID audience, QuestID quest)
    {

        string key = GetKey(audience, quest);

        states[key] = QuestState.Completed;

        PrintProgress();

        Debug.Log($"Сохранено: {key}");
    }

    public bool IsCompleted(AudienceID audience, QuestID quest)
    {
        return states.TryGetValue(GetKey(audience, quest), out var state)
               && state == QuestState.Completed;

    }
    private void PrintProgress()
    {
        int completed = states.Count;
        int total = 2;

        float percent = (float)completed / total * 100f;
        progressBar.value = percent;
        Debug.Log($"Прогресс: {percent}%");
    }

}
