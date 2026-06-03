using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Stairs : Location
{
    public override void Entry()
    {
        base.Entry();
        dialogueTrigger.TriggerDialogue();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
