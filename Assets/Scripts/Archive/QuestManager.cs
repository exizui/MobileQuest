using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

//public class QuestManager : MonoBehaviour
//{
//    [SerializeField] protected Quest[] quests;
//    [SerializeField] private TMP_Text header;
//    [SerializeField] private TMP_Text description;


//    private int current;

//    private void Start()
//    {
//        foreach(var item in quests)
//        {
//            item.Init(this);
//        }

//        //StartQuest(0); //начало первого квеста
//    }

//    public void TryStartSpecificQuest(Quest quest)
//    {
//        if (quests[current] != quest)
//            return;
//        print("Квест почався");
//    }

//    public void NextQuest()
//    {
//        current++;

//        if (current < quests.Length)
//        {
//            StartQuest(current);
//        }
//        else
//        {
//            //добавить чтото надо
//        }

//    }

//    private void StartQuest(int num)
//    {
//        quests[num].Activate();
//        header.text = quests[num].GetHeader();
//    }

//    private void Update()
//    {
//        if(current < quests.Length)
//        {
//            description.text = quests[current].GetDesctiption();
//        }
//    }

//}
