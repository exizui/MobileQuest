using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_1 : Location
{
    public GameObject entryWatch;
    public GameObject standButton;

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
        if (GameState.instance.HasFlag("canStand"))
        {
            ShowStand(true);
        }
        else
        {
            ShowStand(false);
        }
    }
    private void ShowEntry(bool canEntry)
    {
        if (canEntry)
        {
            entryWatch.SetActive(true);
        }
        else
        {
            entryWatch.SetActive(false);
        }
    }
    private void ShowStand(bool canEntry)
    {
        if (canEntry)
        {
            standButton.SetActive(true);
        }
        else
        {
            standButton.SetActive(false);
        }
    }
    public override void Exit()
    {
        base.Exit();
    }
}
