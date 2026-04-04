using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestQuestions : Quest
{
    public Dialogue quiz1;
    private DialogueManager dialog;
    protected override void OnStart()
    {
        Debug.Log("допрос");
        QuizStart();
    }

    private void QuizStart(Action onEnd = null)
    {
        dialog = FindObjectOfType<DialogueManager>();
        dialog.StartDialogue(quiz1, () =>
        {
            onEnd?.Invoke();
        });
        EndQuest();
    }

    protected override void EndQuest()
    {
        LocationNavigator.Controller.ShowExitDoor();
    }
}
