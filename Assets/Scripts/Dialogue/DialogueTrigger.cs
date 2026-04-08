using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public static DialogueTrigger instance;
    //private bool isTalked = false;
    public string dialogueID;
    //public static event Action<string, bool> OnDialogueWas; ///
    //public static event Action<string, bool> OnSave;
    private void Awake()
    {
        instance = this;

    }

    public void TriggerDialogue(Action onEnd = null)
    {
        if (SaveSystem.IsTalked(dialogueID))
        {
            Debug.Log("уже был диалог");
            LocationNavigator.Controller.ShowExitDoor();
            return;
        }
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, () =>
        {
            // диалог закончился → сохраняем
            SaveSystem.SetTalked(dialogueID);
            Debug.Log(dialogueID);
            onEnd?.Invoke();
        });
    }

}
