using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    private Dictionary<string, int>  eventCounts = new Dictionary<string, int>();

    //public event Action<string> OnEventCompleted;

    private void Awake()
    {
        instance = this;
    }

    public void TriggerEvent(string eventID, int requiredCount)
    {
        if (!eventCounts.ContainsKey(eventID)) { eventCounts[eventID] = 0; }

        Debug.Log("Активе тригер " + eventID);

        eventCounts[eventID]++;
        Debug.Log(eventCounts[eventID]);
        print(eventID + " count = " + eventCounts[eventID]);

        if (eventCounts[eventID] >= requiredCount)
        {
            Debug.Log("СОБЫТИЕ ВЫПОЛНЕНО: " + eventID);
            //OnEventCompleted?.Invoke(eventID);
            GameState.instance.SetFlag("canCraft");
        }
    }
}
