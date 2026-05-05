using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="DialogueChanger", menuName ="Dialogue/DialogueChanger")]
public class DialogueChanger : ScriptableObject
{
    [System.Serializable]
    public class ChangeStep
    {
        public string requiredDialogueID;
        public Dialogue newRepeatDialogue; 
    }
    public ChangeStep[] steps;

    public void TryChange(ref Dialogue currentRepeat)
    {
        if (steps == null || steps.Length == 0) return;

        for (int i = steps.Length - 1; i >= 0; i--)
        {
            var step = steps[i];

            if(step == null || step.newRepeatDialogue == null) continue;

            if (SaveSystem.IsTalked(step.requiredDialogueID))
            {
                currentRepeat = step.newRepeatDialogue;
                return;
            }
        }
    }
}
