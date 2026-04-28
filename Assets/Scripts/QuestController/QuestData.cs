using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Quest/QuestData")]
public class QuestData : ScriptableObject 
{
    public string questID;
    public List<LocationID> allowedRooms;
    [TextArea] public string description;

    public QuestStepData[] steps;

    public List <BaseReward> rewards;
}
[System.Serializable]
public class QuestProgressData
{
    public string questID;
    public int currentStep;
}

[System.Serializable]
public class QuestStepData
{
    public QuestStepType stepType;

    public ItemData item;
    public int amount;

    public LocationID locationID;

    public string customStepID;

    [ResizableTextArea]
    public string description;

    public string triggerID;
}
[System.Serializable]
public class QuestSaveData
{
    public List<QuestProgressData> activeQuests = new List<QuestProgressData>();
    public List<string> completedQuests = new List<string>();
}
