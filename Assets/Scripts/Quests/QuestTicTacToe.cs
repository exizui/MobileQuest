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
        TicTacManager.Win += Complete;
    }
    public void StartQuest(Quest quest)
    {
        this.quest = quest;
    }
    public void RestartQuest(Quest quest)
    {

    }
    public void Complete()
    {
        print("MORZHI EBALICSH SELI S OBOSRALISH");
        quest.CompleteCurrentStep();
        TicTacManager.Win -= Complete;
    }
}
