using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTicTacToe : MonoBehaviour, IQuestHandler
{
    public string questid;
    public string QuestID => questid;

    private Quest _quest;

    private void OnEnable()
    {
        TicTacManager.Win += Complete;
    }
    public void StartQuest(Quest quest)
    {
        _quest = quest;
    }

    public void Complete()
    {
        if (_quest == null)
        {
            Debug.LogError("QuestTicTacToe: StartQuest не був викликаний!", this);
            TicTacManager.Win -= Complete;
            return;
        }
        _quest.CompleteCurrentStep();
        TicTacManager.Win -= Complete;
    }
}
