using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestQuestions : MonoBehaviour, IQuestHandler
{
    public Dialogue quiz1;
    private DialogueManager dialog;
    public string questId;
    public string QuestID => questId;

    void Start()
    {
        dialog = FindObjectOfType<DialogueManager>();
    }
    public void StartQuest(Quest quest)
    {
        Debug.Log("допрос");
        QuizStart(Complete);
    }

    private void QuizStart(Action onEnd = null)
    {
        dialog.StartDialogue(quiz1, () =>
        {
            onEnd?.Invoke();
        });
    }

    public void Complete()
    {
        QuestUI.instance.ShowExitDoor();
    }
}
