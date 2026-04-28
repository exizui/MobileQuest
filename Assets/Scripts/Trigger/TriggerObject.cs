using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObject : MonoBehaviour 
{
    private QuestCollect questCollect;
    void Awake()
    {
        questCollect = FindObjectOfType<QuestCollect>();
    }

    private void OnMouseDown()
    {
        if (gameObject.CompareTag("Trigger"))
        {
            Notification.instance.ShowMessage("АААААААААААААААА");
        }
        else
        {
            questCollect.AddProgress();
            gameObject.SetActive(false);
        }
        
    }
}
