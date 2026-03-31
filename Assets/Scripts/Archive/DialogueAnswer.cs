using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//public class DialogueAnswer : MonoBehaviour
//{
//    [SerializeField] private Button[] buttons;
//    [SerializeField] private TMP_Text[] buttonTexts;

//    private DialogueStory _dialogueStory;
//    private void UpdateAnswers(DialogueStory.Story story)
//    {
//        var answers = story.Answers;

//        // 👉 если нет ответов — скрываем панель
//        if (answers == null || answers.Length == 0)
//        {
//            gameObject.SetActive(false);
//            return;
//        }

//        gameObject.SetActive(true);

//        for (int i = 0; i < buttons.Length; i++)
//        {
//            if (i < answers.Length)
//            {
//                buttons[i].gameObject.SetActive(true);
//                buttonTexts[i].text = answers[i].Text;

//                int index = i;

//                buttons[i].onClick.RemoveAllListeners();
//                buttons[i].onClick.AddListener(() =>
//                {
//                    OnAnswerSelected(answers[index]);
//                });
//            }
//            else
//            {
//                buttons[i].gameObject.SetActive(false);
//            }
//        }
//    }

//    private void OnAnswerSelected(DialogueStory.Answer answer)
//    {
//        if (!string.IsNullOrEmpty(answer.NextStoryTag))
//        {
//            _dialogueStory.ChangeStory(answer.NextStoryTag);
//        }
//    }
//}
