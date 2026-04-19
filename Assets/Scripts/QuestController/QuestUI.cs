using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using System;
public class QuestUI : MonoBehaviour
{
    public static QuestUI instance { get; private set; }
    [SerializeField] private TextMeshProUGUI questText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    [SerializeField] private GameObject headerObject;
    [SerializeField] private GameObject descriptObject;
    [SerializeField] private GameObject exitButton;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        headerObject.SetActive(false);
        //descriptObject.SetActive(false);
        //ClearHeader();
        //ClearProgress();
    }
    public void ActiveUI()
    {
        //headerObject.SetActive(true);
        //descriptObject.SetActive(true);
        
    }

    public void DisActiveUI()
    {
        //headerObject.SetActive(false);
        //descriptObject.SetActive(false);
        ClearHeader(); ////  !!!!
        ClearProgress(); ///// !!!!!
    }

    public void ShowHeader(string text)
    {
        if (!headerObject.activeSelf)
        {
            headerObject.SetActive(true);
        }
        questText.text = text;

    }
    public void CompleteQuest(string text)
    {
        questText.text = text;
        StartCoroutine(HideText());
    }
    //public void ShowHeader(string text)
    //{
    //    if (!string.IsNullOrEmpty(questText.text))
    //    {
    //        ShowDescription(text);
    //        return;
    //    }
    //    else
    //    {
    //        questText.text = text;
    //    }

      
    //}
    //public void CompleteQuest(string text)
    //{
    //    if (!string.IsNullOrEmpty(questText.text))
    //    {
    //        ShowDescription(text);
    //        return;
    //    }
    //    else
    //    {
    //        questText.text = text;
    //    }
    //    StartCoroutine(HideText());
    //}

    IEnumerator HideText()
    {
        yield return new WaitForSeconds(2f);
        ClearHeader();
    }
    public void ClearHeader()
    {
        questText.text = "";
    }

    public void ClearProgress()
    {
        descriptionText.text = "";
    }
    public void ShowDescription(string progress)
    {
        descriptionText.text = progress;
    }

    public void ShowExitDoor()
    {
        //StartCoroutine(ShowDoor());
        exitButton.SetActive(true);
    }

    IEnumerator ShowDoor()
    {
        yield return new WaitForSeconds(2f);
        exitButton.SetActive(true);
        ClearProgress();
    }
}
