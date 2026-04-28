using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

//public class QuestCollect : Quest
//{
//    private int int_progress;

//    public GameObject triggerObjectGroup;

//    public ItemData itemKey;

//    protected override void OnStart()
//    {
//        Debug.Log("Квест коллект почався");
//        //triggerObject.ActiveTrigger();
//        triggerObjectGroup.SetActive(true);
//    }

//    public void AddProgress()
//    {
//        int_progress++;

//        if (int_progress == 3)
//        {
//            Complete();
//        }
//    }

//    protected override void EndQuest()
//    {
//        //questUI.ShowHeader("Ви виконали завдання");
//        Notification.Instance.ShowMessage("Ви виконали квест і отримали ключ!");
//        questUI.DisActiveUI();
//        Inventory.instance.AddItem(itemKey);
//        QuestUI.instance.ShowExitDoor();

//        //
//    }
//}
