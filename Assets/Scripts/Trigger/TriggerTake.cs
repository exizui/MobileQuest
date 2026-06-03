using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTake : MonoBehaviour 
{
    public DialogueTrigger dialogueTrueCoffee;
    public DialogueTrigger dialogueFalseCoffee;

    public ItemData[] Coffee;
    public ItemData trueCoffee;
    public void OnClick()
    {
        if (Inventory.instance.HasItem(trueCoffee))
        {
            dialogueTrueCoffee.TriggerDialogue(TakeCoffee);
        }
        else
        {
            dialogueFalseCoffee.TriggerDialogue();
        }
    }

    private void TakeCoffee()
    {
        QuestManager.instance.ItemDelivered(trueCoffee);
        EventManager.instance.TriggerEvent("craft", 3); ///////

        GameState.instance.DeleteFlag("buyCoffee");
        GameState.instance.DeleteFlag("takeDrink");

        gameObject.SetActive(false);

        foreach (var item in Coffee)
        {
            if (Inventory.instance.HasItem(item))
            {
                Inventory.instance.RemoveItem(item);
            }
        }

        QuestUI.instance.ShowExitDoor();
    }
}
