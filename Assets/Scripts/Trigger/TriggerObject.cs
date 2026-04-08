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
        questCollect.AddProgress();
        gameObject.SetActive(false);
    }
}
