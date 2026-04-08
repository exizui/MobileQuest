using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_1 : Locations
{
    [SerializeField] private GameObject key13;
    [Header("Кнопки")]
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject entryButton;
    [SerializeField] private DialogueTrigger dialogueTrigger;
    public override void Entry()
    {
        base.Entry();
        dialogueTrigger = GetComponent<DialogueTrigger>();
        if (TriggerDoor.isFirstOpen)
        {
            if (entryButton != null)
            {
                entryButton.SetActive(true);
            }

        }
    }
    public void ALlowDialogue()
    {
        dialogueTrigger.TriggerDialogue(ActiveKey);
        Debug.Log("АллоДиалог сработал");
    }

    private void ActiveKey()
    {
        key13.SetActive(true);
        exitButton.SetActive(true);
    }
    // Update is called once per frame
    public override void Exit()
    {
        base.Exit();
    }
}
