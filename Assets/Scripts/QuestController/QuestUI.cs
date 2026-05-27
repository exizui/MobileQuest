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
    [SerializeField] private GameObject headerObject;
    [SerializeField] private GameObject exitButton;
    [SerializeField] private GameObject lining_Header;

    public bool IsExitDoorOpen => exitButton.activeSelf;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        headerObject.SetActive(false);
        lining_Header.SetActive(false);

    }
    public void ActiveUI()
    {
        lining_Header.SetActive(true);     
    }

    public void DisActiveUI()
    {
        lining_Header.SetActive(false);
        ClearHeader(); ////  !!!!
        //ClearProgress(); ///// !!!!!
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
        questText.color = Color.green;
        StartCoroutine(HideText());
    }

    IEnumerator HideText()
    {
        yield return new WaitForSecondsRealtime(2f);
        DisActiveUI();
    }
    public void ClearHeader()
    {
        questText.text = "";
        questText.color= Color.white;
    }

    public void ShowExitDoor()
    {
        StartCoroutine(ShowDoor());
        //exitButton.SetActive(true);
    }

    IEnumerator ShowDoor()
    {
        yield return new WaitForSeconds(0.7f);
        exitButton.SetActive(true);
    }

    public void HideExitDoor()
    {
        exitButton.SetActive(false);
    }
}
