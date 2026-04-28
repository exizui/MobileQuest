using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueClickPanel : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private DialogueManager dialogueManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        dialogueManager.DisplayNextSentence();
    }
}