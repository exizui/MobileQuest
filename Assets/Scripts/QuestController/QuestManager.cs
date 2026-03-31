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

    public bool OnLocationEntered(BaseLocations location)
    {
        var questHolder = location.GetComponent<QuestAudience>();

        if (questHolder == null)
            return false;

        foreach (var questID in questHolder.Quests)
        {
            if (!QuestProgress.Instance.IsCompleted(questHolder.audienceID, questID))
            {
                StartQuest(location, questID);
                return true;
            }
        }

        return false;
    }

    private void StartQuest(BaseLocations location, QuestID questID)
    {
        var quests = location.GetComponents<Quest>();

        foreach (var quest in quests)
        {
            if (quest.QuestID == questID)
            {
                quest.Init(location);
                quest.StartQuest();
                break;
            }
        }


    }
}