using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class QuestTest : Quest
{
    [SerializeField] private GameObject pc;
    protected override void OnStart()
    {
         pc.SetActive(true);
    }

    public void TriggerExit()
    {
        LocationNavigator.Controller.ShowExitDoor();
        
    }

    protected override void EndQuest()
    {

    }
}
