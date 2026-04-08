using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerDoor : MonoBehaviour
{
    private Button button;
    public static bool isFirstOpen;
    [SerializeField] private float waitforThink = 2f;
    private DialogueTrigger dialogueTrigger;
    public Button[] nextandprevButton;
    private void Awake()
    {
        button = GetComponent<Button>();
        dialogueTrigger = GetComponent<DialogueTrigger>();
        button.onClick.AddListener(OnCLick);
    }

    private void OnCLick()
    {
        if (!isFirstOpen)
        {
            button.enabled = false;
            nextandprevButton[0].enabled = false;
            nextandprevButton[1].enabled = false;
            StartCoroutine(StartThink());
            isFirstOpen = true;
        }
    }

    IEnumerator StartThink()
    {
        yield return new WaitForSeconds(waitforThink);
        dialogueTrigger.TriggerDialogue(ActiveButton);
    }

    private void ActiveButton()
    {
        nextandprevButton[0].enabled = true;
        nextandprevButton[1].enabled = true;
        button.enabled = true;
        Destroy(this);
    }
}
