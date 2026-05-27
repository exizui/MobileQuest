using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
public class DialogueTrigger : MonoBehaviour
{
    //[Space(6)]
    [Header("Перший діалог")]
    public Dialogue dialogue;
    [Header("Повторний")]
    public Dialogue repeatDialogue;
    [Header("Змінювач діалогу")]
    public DialogueChanger dialogueChanger;

    [Header("Ключ діалогу")]
    public string dialogueID;

    [Header("Діалог з чергою")]
    public DialogueSequence sequence;
    public static DialogueTrigger instance;

    //[SerializeField] private bool showExitOnRepeat = true;

    private static readonly HashSet<string> talkedThisSession = new HashSet<string>(); //
    
    public bool repeatable = false;

    private bool _questMode = false;

    private void Awake() => instance = this;

    public void SetQuestMode()
    {
        _questMode = true;
        Debug.Log($"SetQuestMode на {gameObject.name}");
    }
    private Action _savedOnEnd;
    public void TriggerDialogue(Action onEnd = null)
    {
        //if (dialogueChanger != null)
        //{
        //    dialogueChanger.TryChange(ref repeatDialogue);
        //}
        if (onEnd != null)
            _savedOnEnd = onEnd;

        if (_questMode && sequence != null)
        {
            var next = sequence.GetNext();

            //Action afterDialogue = sequence.IsLast() ? onEnd : null;

            StartDialogue(_savedOnEnd, next);
            return;
        }
        if (sequence != null && dialogue == null)
        {
            var next = sequence.GetNext();
            StartDialogue(onEnd, next);
            return;
        }

        if (!repeatable && SaveSystem.IsTalked(dialogueID))
        {
            RepeatDialogue();
        }
        else
        {
            talkedThisSession.Add(dialogueID);
            StartDialogue(onEnd, dialogue);
        }

    }

    private void RepeatDialogue(Action onEnd = null)
    {
        Debug.Log("уже был диалог");
        QuestUI.instance.ShowExitDoor();
        if (!talkedThisSession.Contains(dialogueID))
            return;
        if (repeatDialogue != null)
        {
            StartDialogue(onEnd, repeatDialogue);
        }


    }

    private void StartDialogue(Action onEnd = null, Dialogue dialogue = null)
    {
        if (dialogue == null)
        {
            Debug.LogWarning("ДІАЛОГ == NULL");
            return;
        }
        DialogueManager manager = FindObjectOfType<DialogueManager>();

        manager.StartDialogue(dialogue, () =>
        {
            SaveSystem.SetTalked(dialogueID);
            onEnd?.Invoke();
        });
    }

    public void StartDirectDialogue(Dialogue dialogue, Action onEnd)
    {
        DialogueManager manager = FindObjectOfType<DialogueManager>();
        manager.StartDialogue(dialogue, onEnd);
    }
}
