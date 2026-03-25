using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class QuestManager : MonoBehaviour
{
    public static QuestManager singleton;

    private void Awake()
    {
        singleton = this;
    }
    private Dictionary<BaseLocations, bool> visitedLocations = new Dictionary<BaseLocations, bool>();

    public bool OnLocationEntered(BaseLocations location) //QuestUI quest)
    {
        if (visitedLocations.ContainsKey(location)) {
            return false; 
        }

        visitedLocations[location] = true;

        //quest.ActiveUI();

        StartQuests(location);
        return true;


    }

    private void StartQuests(BaseLocations location)
    {
        var quests = location.GetComponents<Quest>();

        foreach (var quest in quests)
        {
            quest.Init(location);
            quest.StartQuest();
        }


    }
}
