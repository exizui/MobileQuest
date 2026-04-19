using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemColor
{
    Red,
    Blue,
    Green,
    Yellow
}
public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager instance;

    public int totalItems;
    private int correctCount = 0;

    private void Awake()
    {
        instance = this;
    }

    public void Correct()
    {
        correctCount++;
        Debug.Log("Правильно");
        if (correctCount >= totalItems)
        {
            Debug.Log("Мини-игра завершена");
            OnWin();
        }
    }

    public void Wrong()
    {
        Debug.Log("Неправильно");
    }

    private void OnWin()
    {
        // 👉 сюда вставишь:
        // - завершение квеста
        // - запуск диалога
        // - выход из аудитории

        QuestUI.instance.ShowExitDoor();
    }
}