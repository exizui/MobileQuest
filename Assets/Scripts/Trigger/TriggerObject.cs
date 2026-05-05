using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObject : MonoBehaviour 
{
    private QuestCollect questCollect;
    private bool isUsed;
    void Awake()
    {
        questCollect = FindObjectOfType<QuestCollect>();
    }

    private void OnMouseDown()
    {
        if (isUsed) return;

        if (gameObject.CompareTag("Trigger"))
        {
            Notification.instance.ShowMessage("Не чіпай мої речі!!!");
        }
        else
        {
            isUsed = true;
            questCollect.AddProgress();
            gameObject.SetActive(false);
        }
        
    }

    public void ResetTrigger()
    {
        isUsed = false;
        gameObject.SetActive(true);
    }
}
