using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud_KZ13 : Locations
{

    public override void Entry()
    {
        base.Entry();
        QuestUI.instance.ShowExitDoor();
    }


    public override void Exit()
    {
        base.Exit();

    }


}