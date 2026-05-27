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

    private bool isAnimating;

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
        if (isAnimating) //
            yield break; //

        yield return new WaitForSeconds(dialogueDelay);


        dialogueWindow.SetActive(true);

        isAnimating = true;//

        animator.SetTrigger("Show");//анимація появлення панелі

        yield return new WaitForSeconds(0.2f);//
        isAnimating = false;//

        nameText.text = dialogue.nps_name.name;

        sentences.Clear();
        onDialogueEnd = onEnd; //присвоєння події

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

        //if (answers == null || answers.Length == 0)
        //{
        //    EndDialogue();
        //    return;
        //}

        if (answers != null && answers.Length > 0)
        {
            ShowChoices(answers);
            return;
        }

        if (currentDialogue.nextDialogue != null)
        {
            StartCoroutine(PlayBackANDUpAnimation(currentDialogue.nextDialogue));
            return;
        }

        EndDialogue();
    }

    private IEnumerator PlayBackANDUpAnimation(Dialogue next)
    {
        isAnimating = true;
        yield return PlayDownAnimation();

        isAnimating = false;
        StartDialogue(next, onDialogueEnd);
    }

    private void ShowChoices(Answer[] answers)
    {
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
        if (!isAnimating)
            StartCoroutine(PlayUpAnimation());
    }
    public void GetAnswerButton(int number)
    {
        answerButtons[number].onClick.Invoke();
    }
    private void SelectAnswer(Answer answer)
    {
        if (isAnimating)
            return;

        QuizResult.lastAnswerCorrect = answer.isCorrect;

        choicesPanel.SetActive(false);

        ExecuteAnswerLogic(answer);

        if (answer.nextDialogue != null)
        {
            StartCoroutine(PlayBackAnimation(answer));
        }
        else
        {
            StartCoroutine(DelayedEnd());
        }
    }
    private IEnumerator DelayedEnd()
    {
        yield return new WaitForSeconds(0.5f); // невелика пауза
        EndDialogue();
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
        if (isAnimating)
            return;
        StartCoroutine(PlayDownAnimation());
    }
    private IEnumerator PlayDownAnimation()
    {
        isAnimating = true;

        animator.SetTrigger("Down");

        yield return new WaitForSeconds(0.8f);

        dialogueWindow.SetActive(false);

        isAnimating = false;

        onDialogueEnd?.Invoke();
    }
    private IEnumerator PlayBackAnimation(Answer answer)
    {
        isAnimating = true;

        animator.SetTrigger("Back");

        yield return new WaitForSeconds(0.5f);

        isAnimating = false;

        StartDialogue(answer.nextDialogue, onDialogueEnd);
    }
    private IEnumerator PlayUpAnimation()
    {
        isAnimating = true;

        animator.SetTrigger("Up");

        yield return new WaitForSeconds(0.5f);

        isAnimating = false;
    }
    private IEnumerator StairsTransition(string id)
    {
        yield return new WaitForSeconds(0.5f); // длительность Back

        animator.SetTrigger("Down");

        Stairs.instance.Go_Level(id);
    }
    private IEnumerator EndDialogueDelay()
    {
        yield return new WaitForSeconds(2f);
        dialogueWindow.SetActive(false);
        onDialogueEnd?.Invoke();
    }

    public void SkipDialogue()
    {
        if (isAnimating) return;
        StopAllCoroutines();
        sentences.Clear();
        ShowChoicesOrEnd();  
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
            case AnswerActionType.Stairs:
                //Stairs.instance.Go_Level(answer.actionID);
                StartCoroutine(StairsTransition(answer.actionID));
                break;
        }
    }
}

