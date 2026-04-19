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

    public string TriggerID = "KZ13";
    private KeyManager keyManager;

    private void Awake()
    {
        button = GetComponent<Button>();
        dialogueTrigger = GetComponent<DialogueTrigger>();
        keyManager = GetComponent<KeyManager>();
        button.onClick.AddListener(OnCLick);
    }

    private void OnCLick()
    {
        StartCoroutine(StartThink());
    }

    IEnumerator StartThink()
    {
        keyManager.OffButton();
        yield return new WaitForSeconds(waitforThink);
        dialogueTrigger.TriggerDialogue(ActiveButton);
    }

    private void ActiveButton()
    {
        keyManager.OnButton();
        GameState.instance.SetFlag("tryOpenDoor");
        QuestManager.instance.Trigger(TriggerID);
        Destroy(this);
        Destroy(dialogueTrigger);
    }

}
