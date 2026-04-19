using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public Dialogue repeatDialogue;
    public static DialogueTrigger instance;

    public string dialogueID;

    private void Awake()
    {
        instance = this;

    }
    public bool repeatable = false;

    public void TriggerDialogue(Action onEnd = null)
    {
        if (!repeatable && SaveSystem.IsTalked(dialogueID))
        {
            RepeatDialogue();
        }
        else
        {
            StartDialogue(onEnd, dialogue);
        }

    }

    private void RepeatDialogue(Action onEnd = null)
    {
        Debug.Log("уже был диалог");

        if(repeatDialogue != null)
        {
            StartDialogue(onEnd, repeatDialogue);
        }
        //LocationNavigator.Controller.SetAudienceState();
        QuestUI.instance.ShowExitDoor();
        return;
    }

    private void StartDialogue(Action onEnd = null, Dialogue dialogue = null)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, () =>
        {
            SaveSystem.SetTalked(dialogueID);
            Debug.Log(dialogueID);
            onEnd?.Invoke();
        });
    }
}
