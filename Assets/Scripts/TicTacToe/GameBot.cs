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

    // Попередньо обчислені виграшні комбінації
    private static readonly int[,] winPatterns = new int[,]
    {
        {0,1,2},{3,4,5},{6,7,8},
        {0,3,6},{1,4,7},{2,5,8},
        {0,4,8},{2,4,6}
    };

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start() => ResetBoard();

    public void ResetBoard()
    {
        for (int i = 0; i < board.Length; i++)
            board[i] = Cell.Empty;
    }

    public Cell GetCell(int index) => board[index];

    public bool PlayerMove(int index)
    {
        if (board[index] != Cell.Empty)
            return false;

        board[index] = player;
        return true;
    }

    public void MakeAIMove()
    {
        int move = GetBestMove();
        if (move >= 0)
            board[move] = ai;
    }

    public bool IsBoardFull()
    {
        for (int i = 0; i < board.Length; i++)
            if (board[i] == Cell.Empty)
                return false;
        return true;
    }

    public bool CheckWin(Cell p)
    {
        for (int i = 0; i < 8; i++)
        {
            if (board[winPatterns[i, 0]] == p &&
                board[winPatterns[i, 1]] == p &&
                board[winPatterns[i, 2]] == p)
                return true;
        }
        return false;
    }

    private int GetBestMove()
    {
        // Випадковий хід згідно з difficulty
        if (Random.value > difficulty)
            return GetRandomMove();

        int bestScore = int.MinValue;
        int move = -1;

        for (int i = 0; i < 9; i++)
        {
            if (board[i] == Cell.Empty)
            {
                board[i] = ai;
                int score = MiniMax(false);
                board[i] = Cell.Empty;

                // Невелика випадковість для варіативності
                score += Random.Range(-1, 2);

                if (score > bestScore)
                {
                    bestScore = score;
                    move = i;
                }
            }
        }
        return move;
    }

    private int GetRandomMove()
    {
        //Оптимізований збір доступних ходів
        List<int> moves = new List<int>(9);
        for (int i = 0; i < 9; i++)
        {
            if (board[i] == Cell.Empty)
                moves.Add(i);
        }

        return moves.Count > 0 ? moves[Random.Range(0, moves.Count)] : -1;
    }

    private int MiniMax(bool isMax)
    {
        //Швидка перевірка термінальних станів
        if (CheckWin(ai)) return 1;
        if (CheckWin(player)) return -1;
        if (IsBoardFull()) return 0;

        if (isMax)
        {
            int best = -2; // Використовуємо -2 як менше за можливі значення (-1,0,1)
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == Cell.Empty)
                {
                    board[i] = ai;
                    int score = MiniMax(false);
                    board[i] = Cell.Empty;

                    if (score > best) best = score;
                    if (best == 1) break; // Ранній вихід (знайдено переможний хід)
                }
            }
            return best;
        }
        else
        {
            int best = 2; //Використовуємо 2 як більше за можливі значення (-1,0,1)
            for (int i = 0; i < 9; i++)
            {
                if (board[i] == Cell.Empty)
                {
                    board[i] = player;
                    int score = MiniMax(true);
                    board[i] = Cell.Empty;

                    if (score < best) best = score;
                    if (best == -1) break; // Ранній вихід (знайдено програшний хід)
                }
            }
            return best;
        }
    }

    public int GetWinIndex(Cell p)
    {
        for (int i = 0; i < 8; i++)
        {
            if (board[winPatterns[i, 0]] == p &&
                board[winPatterns[i, 1]] == p &&
                board[winPatterns[i, 2]] == p)
                return i;
        }
        return -1;
    }
}