using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager instance;

    public GameObject testExit;

    public int totalItems;
    public bool requiredColor;

    private int correctCount = 0;
    private HashSet<DragAndDrop> correctItems = new HashSet<DragAndDrop>();

    private void Awake()
    {
        instance = this;
    }

    public void EvaluateItem(DragAndDrop item, DropZone panel)
    {
        bool isCorrect = !requiredColor || item.itemColor == panel.panelColor;

        if (isCorrect)
        {
            if (!correctItems.Contains(item))
            {
                correctItems.Add(item);
                correctCount++;
            }
        }
        else
        {
            if (correctItems.Contains(item))
            {
                correctItems.Remove(item);
                correctCount--;
            }
        }

        CheckWin();
    }

    public void RemoveItem(DragAndDrop item)
    {
        if (correctItems.Contains(item))
        {
            correctItems.Remove(item);
            correctCount--;
        }
    }
    private void CheckWin()
    {
        print($"Correct: {correctCount}/{totalItems}");

        if(correctCount >= totalItems)
        {
            OnWin();
        }
    }
    private void OnWin()
    {
        testExit.SetActive(true);
        GameState.instance.SetFlag("AllowBack");
    }
}