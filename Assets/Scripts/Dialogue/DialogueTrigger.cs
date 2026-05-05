using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DialogueTrigger : MonoBehaviour
{
    [Space(6)]
    [Header("Перший діалог, повторний, змінювач діалогу")]
    public Dialogue dialogue;
    public Dialogue repeatDialogue;
    public DialogueChanger dialogueChanger;

    [Header("Ключ діалогу")]
    public string dialogueID;

    [Header("Діалог з чергою")]
    public DialogueSequence sequence;
    public static DialogueTrigger instance;

    [SerializeField] private bool showExitOnRepeat = true;


    private void Awake()
    {
        instance = this;
    }

    public bool repeatable = false;

    public void TriggerDialogue(Action onEnd = null)
    {
        //if (dialogueChanger != null)
        //{
        //    dialogueChanger.TryChange(ref repeatDialogue);
        //}
        if (sequence != null)
        {
            var nextDialopgue = sequence.GetNext();
            StartDialogue(onEnd, nextDialopgue);
            return;
        }

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
        if (showExitOnRepeat)
        {
            QuestUI.instance.ShowExitDoor();
        }

        return;
    }

    private void StartDialogue(Action onEnd = null, Dialogue dialogue = null)
    {
        if (dialogue == null)
        {
            Debug.LogWarning("Dialogue is NULL");
            return;
        }
        DialogueManager manager = FindObjectOfType<DialogueManager>();

        manager.StartDialogue(dialogue, () =>
        {
            SaveSystem.SetTalked(dialogueID);
            Debug.Log(dialogueID);
            onEnd?.Invoke();
        });
    }
}
