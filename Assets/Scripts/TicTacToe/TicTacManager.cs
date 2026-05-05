using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TicTacManager : MonoBehaviour
{
    public static TicTacManager instance;

    public GameBot game;
    public BoardUI boardUI;

    [Header("O і Х sprites")]
    public Sprite X;
    public Sprite O;

    public GameObject winPanel;
    public TextMeshProUGUI winText;
    public float waitMoveBot = 0.5f;

    public bool IsPlayerTurn { get; private set; }

    public static Action Win;

    public GameObject exitB;
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        boardUI.InitSprites(X, O);
        StartGame();
    }

    public void StartGame()
    {
        boardUI.InitSprites(X, O); ////
        game.ResetBoard();
        boardUI.Refresh();

        winPanel.SetActive(false);
        boardUI.SetAllInteractable(true);

        IsPlayerTurn = true;
    }
    public void OnPlayerMoved()
    {
        boardUI.Refresh();
        if (CheckEnd()) return;

        IsPlayerTurn = false;
        StartCoroutine(BotMove());
    } 
    IEnumerator BotMove()
    {
        yield return new WaitForSeconds(waitMoveBot);

        game.MakeAIMove();
        boardUI.Refresh();

        if(CheckEnd()) yield break;
        IsPlayerTurn = true;
    }
    bool CheckEnd()
    {
        int winIndex = game.GetWinIndex(game.player);
        if (game.CheckWin(game.player))
        {
            OnWin();
            return true;
        }

        if (game.CheckWin(game.ai))
        {
            OnLose();
            return true;
        }

        if (game.IsBoardFull())
        {
            ShowResult("Ничія", Color.gray);
            return true;
        }

        return false;
    }
    void ShowResult(string text, Color color)
    {
        winPanel.SetActive(true);
        winText.text = text;
        winText.color = color;
        

    }

    public void OnWin()
    {
        ShowResult("Ты виграв", Color.green);
        exitB.SetActive(true);
        Win?.Invoke();
    }
    public void OnLose()
    {
        ShowResult("Ты програв", Color.red);
    }
    public void Restart()
    {
        StartGame();
    }
    
}
