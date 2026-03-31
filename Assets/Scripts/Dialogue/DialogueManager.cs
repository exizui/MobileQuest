using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogueManager : MonoBehaviour
{
    private Action onDialogueEnd;
    [Header("DIALOGUE")]
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;

    [SerializeField] private GameObject dialogueWindow;
    [SerializeField] private GameObject choicesPanel;

    [SerializeField] private Button[] answerButtons;
    [SerializeField] private TextMeshProUGUI[] answerTexts;

    private Queue<string> sentences;

    [Header("TextSpeed")]
    [SerializeField] private float dialogueDelay;
    [SerializeField] private float typingSpeed = 0.04f;

    private Dialogue currentDialogue;

    private void Start()
    {
        sentences = new Queue<string>();
        dialogueWindow.SetActive(false);
        choicesPanel.SetActive(false);  
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
        if(dialogue == null)
        {
            Debug.LogError("ScriptblObj dont instance!!!");
            yield break;
        }
        yield return new WaitForSeconds(dialogueDelay);

        dialogueWindow.SetActive(true);
        nameText.text = dialogue.nps_name.name;

        sentences.Clear();
        onDialogueEnd = onEnd; //присваивание события

        currentDialogue = dialogue;

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        choicesPanel.SetActive(false);

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            //EndDialogue();
            ShowChoicesOrEnd();
            return;

        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    private void ShowChoicesOrEnd()
    {
        var answers = currentDialogue.answers;

        if (answers == null || answers.Length == 0)
        {
            EndDialogue();
            return;
        }

        choicesPanel.SetActive(true);
        choicesPanel.transform.SetAsLastSibling();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < answers.Length)
            {
                answerButtons[i].gameObject.SetActive(true);
                answerTexts[i].text = answers[i].text;

                int index = i;

                answerButtons[i].onClick.RemoveAllListeners();
                answerButtons[i].onClick.AddListener(() =>
                {
                    SelectAnswer(answers[index]);
                });
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void SelectAnswer(Answer answer)
    {
        choicesPanel.SetActive(false);

        if (answer.nextDialogue != null)
        {
            StartDialogue(answer.nextDialogue, onDialogueEnd);
        }
        else
        {
            EndDialogue();
        }
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
