using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTicTacToe : MonoBehaviour, IQuestHandler
{
    public string questid;
    public string QuestID => questid;

    private Quest quest;

    private void OnEnable()
    {
        GameManager.Win += Complete;
    }
    public void StartQuest(Quest quest)
    {
        this.quest = quest;
    }

    public void Complete()
    {
        print("MORZHI EBALICSH SELI S OBOSRALISH");
        quest.CompleteCurrentStep();
        GameManager.Win -= Complete;
    }
}
