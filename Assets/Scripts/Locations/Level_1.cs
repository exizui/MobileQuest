using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_1 : Locations
{
    public GameObject entryWatch;

    public override void Entry()
    {
        base.Entry();

        if (GameState.instance.HasFlag("tryOpenDoor"))
        {
            ShowEntry(true);
        }
        else
        {
            ShowEntry(false);
        }
    }
    private void ShowEntry(bool canEntry)
    {
        if (canEntry)
        {
            entryWatch.SetActive(true);
            print("TRUE");
        }
        else
        {
            entryWatch.SetActive(false);
            print("FALSE");
        }
    }
   
    public override void Exit()
    {
        base.Exit();
    }
}
