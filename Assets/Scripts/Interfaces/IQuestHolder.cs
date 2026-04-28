using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//interface IQuestHolder 
//{
//    string HolderID { get; }
//    List<QuestID> Quests { get; }

//}

//public interface IQuestStepHandler
//{
//    string StepID { get; }
//    void StartStep(Quest quest);
//}

public interface IQuestHandler
{
    string QuestID { get; }
    void StartQuest(Quest quest);
    void Complete();
}
