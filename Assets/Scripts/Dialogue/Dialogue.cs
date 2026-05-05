using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName ="Dialogue/NewDialogue")]
public class Dialogue : ScriptableObject
{
    public NPC nps_name;

    [TextArea(3, 10)]

    public string[] sentences;

    public Answer[] answers;
}

[System.Serializable]
public class Answer
{
    public string text;
    public Dialogue nextDialogue;

    [Header("Optional Logic")]
    public AnswerActionType actionType = AnswerActionType.None;
    public string actionID;
    public ItemData item;
}
