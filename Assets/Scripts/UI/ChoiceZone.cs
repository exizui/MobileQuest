using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceZone : MonoBehaviour
{
    public int index;

    private DialogueManager dialogueManager;
    private void Awake()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Handle"))
        {
            dialogueManager.GetAnswerButton(index);
        }
    }

}
