using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTest : Quest
{
    protected override void OnStart()
    {
        Debug.LogWarning("TEST START");
        Exit();
    }


    public void Exit()
    {
        EndQuest();
        Complete();
    }

    

    protected override void EndQuest()
    {
        LocationNavigator.Controller.ShowExitDoor();
        Debug.LogWarning("TEST EXIT");
    }
}
