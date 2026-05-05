using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySecurity : MonoBehaviour
{
    public static InventorySecurity instance;

    public ItemData[] coffee;
    public DialogueTrigger message;

    private int entryCount = 0;

    public event Action OnMessageActive;
    private void Awake()
    {
        instance = this;
    }
    public void CheckInv()
    {
        if (entryCount == 5)
        {
            message.TriggerDialogue(ShowNotification);
            Inventory.instance.RemoveAll(coffee);
            OnMessageActive?.Invoke();
        }
    }

    private void ShowNotification()
    {
        Notification.instance.ShowMessage("Інвентар було очищено");
    }

    public void EnterShop()
    {
        entryCount++;
    }

}
