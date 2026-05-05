using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueSequence")]
public class DialogueSequence : ScriptableObject
{
    public string sequenceID;
    public Dialogue[] dialogues;

    public Dialogue GetNext()
    {
        int index = SaveSystem.GetInt(sequenceID, 0);

        if (dialogues == null || dialogues.Length == 0)
            return null;

        if (index >= dialogues.Length)
            index = dialogues.Length - 1; // зафиксировать на последнем

        Dialogue result = dialogues[index];

        SaveSystem.SetInt(sequenceID, index + 1);

        return result;
    }
}
