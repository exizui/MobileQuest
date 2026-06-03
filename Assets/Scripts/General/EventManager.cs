using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EventManagerSaveData
{
    public List<string> eventIDs;
    public List<int> counts;
}
public class EventManager : MonoBehaviour, ISaveable
{
    public static EventManager instance;

    private Dictionary<string, int> eventCounts = new Dictionary<string, int>();

    private void Awake()
    {
        instance = this;
    }

    public void TriggerEvent(string eventID, int requiredCount)
    {
        TriggerEventWithFlag(eventID, requiredCount, null);
    }
    public void TriggerEvent(string eventID, int requiredCount, string flagToSet)
    {
        TriggerEventWithFlag(eventID, requiredCount, flagToSet);
    }
    public void TriggerEventWithFlag(string eventID, int requiredCount, string flagToSet)
    {
        if (!eventCounts.ContainsKey(eventID))
        {
            eventCounts[eventID] = 0;
        }

        Debug.Log($"Активація тригера {eventID}, цель: {requiredCount}, флаг: {flagToSet ?? "нет"}");

        eventCounts[eventID]++;
        Debug.Log($"{eventID} count = {eventCounts[eventID]}");

        if (eventCounts[eventID] >= requiredCount)
        {
            Debug.Log($"ПОДІЯ ВИКОНАНА: {eventID}");

            // Устанавливаем указанный флаг или используем eventID как флаг
            string flag = flagToSet ?? eventID;
            GameState.instance.SetFlag(flag);

            // Опционально: удаляем счетчик, чтобы событие не сработало повторно
            // eventCounts.Remove(eventID);
        }
    }

    public object CaptureState()
    {
        return new EventManagerSaveData
        {
            eventIDs = new List<string>(eventCounts.Keys),
            counts = new List<int>(eventCounts.Values)
        };
    }

    public void RestoreState(object state)
    {
        var data = (EventManagerSaveData)state;

        eventCounts.Clear();

        for(int i = 0; i < data.eventIDs.Count; i++)
        {
            eventCounts[data.eventIDs[i]] = data.counts[i];
        }
    }
}
