using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    private Action onDialogueEnd;

    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;

    public GameObject dialogueWindow;

    private Queue<string> sentences;

    [SerializeField] private float dialogueDelay;
    private float typingSpeed = 0.04f;

    private void Start()
    {
        sentences = new Queue<string>();
        dialogueWindow.SetActive(false);

    }
    #region Старый метод старта диалога
    /*
    public void StartDialogue(Dialogue dialogue, Action onEnd = null)//добавили событие
    {

        dialogueWindow.SetActive(true);
        nameText.text = dialogue.name;

        sentences.Clear();
        onDialogueEnd = onEnd; //присваивание события

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    */
    #endregion

    public void StartDialogue(Dialogue dialogue, Action oneEnd = null)
    {
        StartCoroutine(StartDialogueWithDelay(dialogue, oneEnd));
    }
    private IEnumerator StartDialogueWithDelay(Dialogue dialogue, Action onEnd = null)
    {
        yield return new WaitForSeconds(dialogueDelay);

        dialogueWindow.SetActive(true);
        nameText.text = dialogue.name;

        sentences.Clear();
        onDialogueEnd = onEnd; //присваивание события

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;

        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void EndDialogue()
    {
        dialogueWindow.SetActive(false);

        onDialogueEnd?.Invoke();
    }
}
