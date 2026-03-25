using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public static DialogueTrigger instance;

    private void Awake()
    {
        instance = this;

    }

    public void TriggerDialogue(Action onEnd = null)
    {

        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, onEnd);
    }
}
