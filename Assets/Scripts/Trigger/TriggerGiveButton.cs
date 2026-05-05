using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TriggerGiveButton : MonoBehaviour
{
    private Button button;
    public ItemData pc;
    private DialogueTrigger dialog;
    //public QuestFindKey quest;
    //public ItemData keyPart;
    public GameObject minigameButton;
    private void Awake()
    {
        button = GetComponent<Button>();
        dialog = GetComponent<DialogueTrigger>();
    }

    public void TryGiveItem()
    {
        if (Inventory.instance.HasItem(pc))
        {
            dialog.TriggerDialogue(DeliveryItem);
        }
        else
        {
            Notification.instance.ShowMessage("Схоже у вас немає пк в руках!");
        }
    }

    private void DeliveryItem()
    {
        Inventory.instance.RemoveItem(pc);
        QuestManager.instance.ItemDelivered(pc);
        //Inventory.instance.AddItem(keyPart);
        EventManager.instance.TriggerEvent("craft", 3); ////
        minigameButton.SetActive(true);
        //QuestUI.instance.ShowExitDoor();
        Destroy(gameObject);
    }

}
