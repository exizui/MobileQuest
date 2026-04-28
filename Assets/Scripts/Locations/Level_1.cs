using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_1 : Locations
{
    public GameObject entryButton;

    public override void Entry()
    {
        base.Entry();

        if (GameState.instance.HasFlag("tryOpenDoor"))
        {
            ShowEntry();
        }
    }


    private void ShowEntry()
    {
        if (entryButton != null)
        {
            entryButton.SetActive(true);
        }
    }
   
    public override void Exit()
    {
        base.Exit();

    }
}
