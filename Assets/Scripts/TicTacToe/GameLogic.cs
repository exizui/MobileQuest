using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GameLogic : MonoBehaviour
{
    public GameObject[] crosses = new GameObject[9];
    public GameObject[] zeros = new GameObject[9];

    public GameObject redLine, blueLine, line = null;

    public TextMeshProUGUI text;

    public Color crossesColor, zerosColor, defaultColor;

    public int[] moves = new int[9];

    private int steps, a,b;

    private float time_for_reflection;

    private bool _isFirst, _isYourRound;

    void Start()
    {
        time_for_reflection = Random.Range(1f, 3f);
        if(Random.Range(0f, 1) <= 0.5f)
        {
            _isFirst = _isYourRound = false;
        }
        else
        {
            _isFirst = _isYourRound = true;
        }
    }

    void Update()
    {
        if (_isYourRound && steps < 9 && !redLine.activeSelf && !blueLine.activeSelf)
        {
            RedactText("Хід оппонента");
            if(time_for_reflection <= 0f)
            {
                do
                {
                    a = Random.Range(0, 9);
                } while (moves[a] != 0);
                Move();
            }
        }
        
    }


    private void RedactText(string txt)
    {
        text.text = txt;
        if (txt == "\nНічия")
            text.color = defaultColor;
        else if(_isFirst && _isYourRound || _isFirst && !_isYourRound)
        {
            text.color = crossesColor;
        }
        else
        {
            text.color = zerosColor;
        }
    }


    private void Move()
    {
        if(_isFirst && _isYourRound)
        {
            moves[b] = 2;
            crosses[b].SetActive(true);
        }
        else if(_isFirst && _isYourRound)
        {
            moves[b] = 1;
            zeros[b].SetActive(true);
        }
        else
        {
            moves[a] = 2;
            crosses[a].SetActive(true);
        }
        steps++;
        Check();
        _isYourRound = !_isYourRound;
    }

    private void Check()
    {
        for(int i = 1;  i < 3; i++)
        {
          
        }
    }

    void End(int numVariant)
    {

    }
}
