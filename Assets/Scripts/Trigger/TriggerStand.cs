using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStand : MonoBehaviour
{
    private string TriggerID = "Final";

    private QuestQuestions quiz;
    public GameObject STAND;

    private void Awake()
    {
        quiz = FindObjectOfType<QuestQuestions>();
    }
    public void OnClick()
    {
        QuestManager.instance.Trigger(TriggerID);
        gameObject.SetActive(false);
        STAND.SetActive(true);
    }

}
