using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTake : MonoBehaviour 
{
    public ItemData trueCoffee;
    public DialogueTrigger dialogueTrueCoffee;
    public DialogueTrigger dialogueFalseCoffee;

    //public ItemData glue;

    //public static event Action On
    public void OnClick()
    {
        if (Inventory.instance.HasItem(trueCoffee))
        {
            dialogueTrueCoffee.TriggerDialogue(TakeCoffee);
            QuestManager.instance.ItemDelivered(trueCoffee);
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
        //Inventory.instance.AddItem(glue); //////
        GameState.instance.DeleteFlag("buyCoffee");
        Destroy(gameObject);
    }
}
