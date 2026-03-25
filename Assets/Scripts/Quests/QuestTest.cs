using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTest : Quest
{
    protected override void OnStart()
    {
        Debug.LogWarning("TEST START");
    }


    public void Exit()
    {
        EndQuest();
    }
    protected override void EndQuest()
    {
        Debug.LogWarning("TEST EXIT");
    }
}
