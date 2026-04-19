using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_1 : Locations
{
    [SerializeField] private GameObject key13;
    [Header("Кнопки")]
    public GameObject entryButton;
    public GameObject backButton;


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
    public void ALlowDialogue()
    {
        dialogue.TriggerDialogue(ActiveKey);
        Debug.Log("АллоДиалог сработал");
    }

    private void ActiveKey()
    {
        key13.SetActive(true);
        backButton.SetActive(true);
    }
    public override void Exit()
    {
        base.Exit();

    }
}
