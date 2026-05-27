using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableButton : MonoBehaviour
{
    private float wait = 0.6f;

    public string TriggerID;
    public void DefaultDisable()
    {
        gameObject.SetActive(false);
        //StartCoroutine(DelayDisable());
    }

    public void DelayDisable()
    {
        StartCoroutine(_DelayDisable());
    }

    public void TriggerDisable()
    {
        Trigger(TriggerID);
    }

    private void Trigger(string triggerID)
    {
       QuestManager.instance.Trigger(triggerID);
       gameObject.SetActive(false);
    }

    IEnumerator _DelayDisable()
    {
        yield return new WaitForSecondsRealtime(wait);
        gameObject.SetActive(false);
    }
}
