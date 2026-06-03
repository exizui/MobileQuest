using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestQuestions : MonoBehaviour, IQuestHandler
{
    public DialogueTrigger quiz;
    private int _currentIndex = 0;
    private Quest _quest;
    private bool _completed = false;

    public string questID;
    public string QuestID => questID;

    public QuizQuestion[] questions;

    public void StartQuest(Quest quest)
    {
        _quest = quest;
        _currentIndex = 0;
        _completed = false;

        AskCurrect();

        Debug.Log("StartQuest вызван");
    }

    private void AskCurrect()
    {
        if (_completed)
            return;

        if (_currentIndex >= questions.Length)
        {
            Complete();
            return;
        }

        var q = questions[_currentIndex];

        quiz.StartDirectDialogue(q.question, OnAnswered);
    }

    private void OnAnswered()
    {
        if (_completed)
            return;

        bool correct = QuizResult.lastAnswerCorrect;

        var q = questions[_currentIndex];

        if (correct)
        {
            _currentIndex++;

            if (_currentIndex >= questions.Length)
            {
                quiz.StartDirectDialogue(q.correct, Complete);
            }
            else
            {
                quiz.StartDirectDialogue(q.correct, AskCurrect);
            }
        }
        else
        {
            quiz.StartDirectDialogue(q.wrong, AskCurrect);
        }
    }

    public void Complete()
    {
        if (_completed)
            return;

        _completed = true;

        _quest.CompleteCurrentStep();

        Debug.Log("COMPLETECURRENTSTEP");

        QuestUI.instance.ShowExitDoor();

        GameState.instance.SetFlag("canStand");
    }
}
