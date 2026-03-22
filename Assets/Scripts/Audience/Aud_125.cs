using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud_125 : Audience
{
    QuestCollect quest;
    QuestUI questUI;
    QuestManager questManager;
    //private bool isQuest = false;
    private void Awake()
    {
        quest = GetComponent<QuestCollect>();
        questUI = FindObjectOfType<QuestUI>();
        questManager = FindObjectOfType<QuestManager>();   
    }

    public override void Entry()
    {
        base.Entry();
        Debug.Log("125");
        //if (!isQuest)
        //{
        //    questUI.ActiveUI();
        //    quest.StartQuest();
        //    isQuest = true;
        //}
        QuestManager.singleton.OnLocationEntered(this, questUI);
        //questUI.ActiveUI();
    }


    public override void Exit()
    {
        base.Exit();
        questUI.DisActiveUI();
    }
}
