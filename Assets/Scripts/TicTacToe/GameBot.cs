using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;

public class GameBot : MonoBehaviour
{
    public static GameBot instance;

    [Range(0f, 1f)]
    public float difficulty = 0.7f;

    public Cell player = Cell.X;
    public Cell ai = Cell.O;

    private Cell[] board = new Cell[9];

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        ResetBoard();
    }

    public void ResetBoard()
    {
        for (int i = 0; i < board.Length; i++)
            board[i] = Cell.Empty;
    }
    public Cell GetCell(int index)
    {
        return board[index];
    }

    public bool PlayerMove(int index)
    {
        if (board[index] != Cell.Empty) return false;

        board[index] = player;

        return true;
    }

    public void MakeAIMove()
    {
        int move = GetBestMove();
        if(move >= 0)
        {
            board[move] = ai;
        }
    }

    public bool IsBoardFull()
    {
        foreach(var c in board)
        {
            if(c == Cell.Empty) return false;
        }
        return true;
    }

    public bool CheckWin(Cell p)
    {
        int[,] w =
        {
            {0,1,2},{3,4,5},{6,7,8},
            {0,3,6},{1,4,7},{2,5,8},
            {0,4,8},{2,4,6}
        };
        for (int i = 0; i < 8; i++)
        {
            if (board[w[i, 0]] == p &&
                board[w[i, 1]] == p &&
                board[w[i, 2]] == p)
                return true;
        }
        return false;
    }

    int GetBestMove()
    {
        if (Random.value > difficulty)
            return GetRandomMove();

        int bestScore = int.MinValue;
        int move = -1;

        for(int i = 0; i < 9; i++)
        {
            if (board[i] == Cell.Empty)
            {
                board[i] = ai;
                int score = MiniMax(false);
                board[i] = Cell.Empty;

                score += Random.Range(-1, 2);

                if(score > bestScore)
                {
                    bestScore = score;
                    move = i;
                }
            }
        }
        return move;
    }

    int GetRandomMove()
    {
        List<int> moves = new List<int>();

        for(int i = 0; i < 9; i++) 
            if(board[i] == Cell.Empty)
                moves.Add(i);

        return moves[Random.Range(0, moves.Count)];
        
    }

    int MiniMax(bool isMax)
    {
        if (CheckWin(ai)) return 1;
        if (CheckWin(player)) return -1;
        if (IsBoardFull()) return 0;

        if (isMax)
        {
            int best = int.MinValue;

            for (int i = 0; i<9; i++)
            {
                if(board[i] == Cell.Empty)
                {
                    board[i] = ai;
                    int score = MiniMax(false);
                    board[i] = Cell.Empty;

                    best = Mathf.Min(best, score);
                }
            }
            return best;
        }
        else
        {
            int best = int.MaxValue;

            for(int i = 0; i < 9; i++)
            {
                if (board[i] == Cell.Empty)
                {
                    board[i] = player;
                    int score = MiniMax(true);
                    board[i] = Cell.Empty;

                    best = Mathf.Max(best, score);
                }
            }
            return best;
        }
       
    }

    public int GetWinIndex(Cell p)
    {
        int[,] w =
        {
            {0,1,2},{3,4,5},{6,7,8},
            {0,3,6},{1,4,7},{2,5,8},
            {0,4,8},{2,4,6}
        };

        for (int i = 0; i< 8; i++)
        {
            if (board[w[i,0]] == p &&
                board[w[i,1]] == p &&
                board[w[i,2]] == p)
                return i;
        }
        return -1;
    }
}
