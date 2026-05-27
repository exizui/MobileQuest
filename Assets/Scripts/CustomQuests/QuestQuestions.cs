using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestQuestions : MonoBehaviour, IQuestHandler
{
    public DialogueTrigger quiz;
    private int _currentIndex = 0;
    private Quest _quest;
    public GameObject standButton;
    public string questID;
    public string QuestID => questID;

    public QuizQuestion[] questions;
    public void StartQuest(Quest quest)
    {
        _quest = quest;
        _currentIndex = 0;
        AskCurrect();
    }

    private void AskCurrect()
    {
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
        bool correct = QuizResult.lastAnswerCorrect;

        if (correct)
        {
            var q = questions[_currentIndex];
            _currentIndex++;
            quiz.StartDirectDialogue(q.correct, AskCurrect);
        }
        else
        {
            var q = questions[_currentIndex];
            quiz.StartDirectDialogue(q.wrong, AskCurrect);
        }

    }

    public void Complete()
    {
        _quest.CompleteCurrentStep();
        QuestUI.instance.ShowExitDoor();
        standButton.SetActive(true);
        print(",,,");
    }
}
