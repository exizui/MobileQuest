using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerShowExit :MonoBehaviour
{
    private QuestTest quest;
    public ItemData item;
    private void OnMouseDown()
    {
        quest = FindObjectOfType<QuestTest>();
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        ItemData newItem = Instantiate(item);
        newItem.icon = sr.sprite;

        if (Inventory.instance.AddItem(item))
        {
            quest.TriggerExit();
            Destroy(gameObject);
            
        }
    }
}
