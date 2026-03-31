using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public static DialogueTrigger instance;
    private bool isTalked = false;
    public string dialogueID;
    //public static event Action<string, bool> OnDialogueWas; ///

    private void Awake()
    {
        instance = this;

    }

    public void TriggerDialogue(Action onEnd = null)
    {
        if (!isTalked)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue, onEnd);
           
            //OnDialogueWas?.Invoke(dialogueID, isTalked); ////
        }
        else
        {
            LocationNavigator.Controller.ShowExitDoor();
        }
        isTalked = true;
    }

    //public bool IsTalked()
    //{
    //    return isTalked;
    //}
}
