using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerFinalDoor : MonoBehaviour
{
    private int requiredCompletedQuests = 0;
    private Button button;
    private KeyManager keyManager;

    private void Start()
    {
        keyManager = GetComponent<KeyManager>();
        button = GetComponent<Button>();

        if (GameState.instance.HasFlag("152"))
            button.interactable = false;
    }
    public void OnClick()
    {
        int completed = QuestManager.instance.GetCompletedCount();

        if (completed >= requiredCompletedQuests )
        {
            keyManager.TryOpenDoor();
            GameState.instance.SetFlag("152");
            button.interactable = false;
        }
        else
        {
            Notification.instance.ShowMessage("Ви не виконали всі квести!");
            Debug.LogError("COMPLETED QUEST" + completed);
        }
    }
}
