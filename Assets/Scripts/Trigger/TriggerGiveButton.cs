using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TriggerGiveButton : MonoBehaviour
{
    public ItemData pc;
    private DialogueTrigger dialog;
    public GameObject minigameButton;
    public GameObject buttonObj;
    private void Awake()
    {
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
        EventManager.instance.TriggerEvent("craft", 3); ////
        minigameButton.SetActive(true);
        //gameObject.SetActive(false);
        GameState.instance.DeleteFlag("tryOpenDoor");
    }

}
