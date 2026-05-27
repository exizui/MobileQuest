using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IQuestHandler
{
    string QuestID { get; }
    void StartQuest(Quest quest);
    void Complete();
}
