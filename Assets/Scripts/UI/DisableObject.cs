using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region OLD DISABLE
//public class DisableButton : MonoBehaviour
//{
//    private float wait = 0.6f;

//    public string TriggerID;
//    public void DefaultDisable()
//    {
//        gameObject.SetActive(false);
//        //StartCoroutine(DelayDisable());
//    }

//    public void DelayDisable()
//    {
//        StartCoroutine(_DelayDisable());
//    }

//    public void TriggerDisable()
//    {
//        Trigger(TriggerID);
//    }

//    private void Trigger(string triggerID)
//    {
//       QuestManager.instance.Trigger(triggerID);
//       gameObject.SetActive(false);
//    }

//    IEnumerator _DelayDisable()
//    {
//        yield return new WaitForSecondsRealtime(wait);
//        gameObject.SetActive(false);
//    }
//}
#endregion
public class DisableObject : MonoBehaviour
{
    private float wait = 0.6f;
    public string TriggerID;

    private string FlagKey => $"disabled_{gameObject.scene.name}_{gameObject.name}";
    private bool _initDone = false;

    private void Start()
    {
        if (GameState.instance.HasFlag(FlagKey))
        {
            gameObject.SetActive(false);
            return;
        }
        StartCoroutine(AllowOnEnableTracking());
    }

    private IEnumerator AllowOnEnableTracking()
    {
        yield return null;
        _initDone = true;
    }

    private void OnEnable()
    {
        if (!_initDone) return;
        GameState.instance?.DeleteFlag(FlagKey);
    }

    public void DefaultDisable()
    {
        GameState.instance.SetFlag(FlagKey);
        gameObject.SetActive(false);
    }

    public void DelayDisable()
    {
        StartCoroutine(_DelayDisable());
    }

    public void TriggerDisable()
    {
        GameState.instance.SetFlag(FlagKey);
        QuestManager.instance.Trigger(TriggerID);
        gameObject.SetActive(false);
    }

    private IEnumerator _DelayDisable()
    {
        yield return new WaitForSecondsRealtime(wait);
        GameState.instance.SetFlag(FlagKey);
        gameObject.SetActive(false);
    }
}

