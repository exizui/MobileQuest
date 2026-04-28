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
    public Animator animator;

    private const float hideWindowDelay = 1.5f;
    private void Start()
    {
        sentences = new Queue<string>();
        dialogueWindow.SetActive(false);
        choicesPanel.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue, Action oneEnd = null)
    {
        StartCoroutine(StartDialogueWithDelay(dialogue, oneEnd));
    }
    private IEnumerator StartDialogueWithDelay(Dialogue dialogue, Action onEnd = null)
    {
        if (dialogue == null)
        {
            Debug.LogError("ScriptblObj dont instance!!!");
            yield break;
        }
        yield return new WaitForSeconds(dialogueDelay);

        dialogueWindow.SetActive(true);
        animator.SetTrigger("Show"); //анимация видвигания

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
        if (sentences.Count == 0)
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
        animator.SetTrigger("Up"); //анимация вверх 
    }

    public void GetAnswerButton(int number)
    {
        answerButtons[number].onClick.Invoke();
    }
    private void SelectAnswer(Answer answer)
    {
        choicesPanel.SetActive(false);

        ExecuteAnswerLogic(answer);

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
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }

    private void EndDialogue()
    {
        animator.SetTrigger("Down");

        StartCoroutine(EndDialogueDelay());
    }

    private IEnumerator EndDialogueDelay()
    {
        yield return new WaitForSeconds(0f);
        dialogueWindow.SetActive(false);
        onDialogueEnd?.Invoke();
    }

    private void ExecuteAnswerLogic(Answer answer)
    {
        if (answer.actionType == AnswerActionType.None)
            return;

        switch (answer.actionType)
        {
            case AnswerActionType.GiveItem:
                Inventory.instance?.AddItem(answer.item);
                break;
        }
    }
}

