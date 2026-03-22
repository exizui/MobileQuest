using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;

public class QuestCollect : Quest
{
    [SerializeField]
    private TextMeshProUGUI tmp_progress;
    [SerializeField]
    private TextMeshProUGUI header;
    private int int_progress;
    //[SerializeField][TextArea]
    //private string description;
    private TriggerObject triggerObject;

    private void Awake()
    {
        triggerObject = FindObjectOfType<TriggerObject>();
    }
    //public override void StartQuest()
    //{
    //    Debug.Log("Квест коллект почався");
    //    UpdateUI(description); 
    //    triggerObject.ActiveTrigger();
    //}

    protected override void OnStart()
    {
        Debug.Log("Квест коллект почався");
        triggerObject.ActiveTrigger();
    }
    private void UpdateUI(string text)
    {
        header.text = text;
        tmp_progress.text = "";
    }

    public void AddProgress()
    {
        int_progress++;
        if (tmp_progress != null)
        {
            tmp_progress.text = int_progress.ToString();
            if(int_progress == 6)
            {
                Complete();
                EndQuest();
            }
        }
    }

    public override void EndQuest()
    {
        QuestUI.instance.Show("Квест зроблений!!! Молодець!!!");
    }
}
