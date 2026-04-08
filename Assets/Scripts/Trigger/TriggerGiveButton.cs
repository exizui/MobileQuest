using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TriggerGiveButton : MonoBehaviour
{
    private Button button;
    public ItemData pc;
    private DialogueTrigger dialog;
    private void Awake()
    {
        button = GetComponent<Button>();
        dialog = GetComponent<DialogueTrigger>();
    }

    public void TryGiveItem()
    {
        if (Inventory.instance.HasItem(pc))
        {
            //Debug.Log("Хочу удалить: " + pc.itemName);
            Inventory.instance.RemoveItem(pc);
            dialog.TriggerDialogue();
        }
        else
        {
            Notification.Instance.ShowMessage("Схоже у вас немає пк в руках!");
        }
    }

}
