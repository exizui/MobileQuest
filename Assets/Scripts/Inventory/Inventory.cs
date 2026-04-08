using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public InventorySlotUI[] slots = new InventorySlotUI[5];

    public static Inventory instance;
    private void Awake()
    {
        instance = this;
    }

    public bool AddItem(ItemData item)
    {
        foreach (var slot in slots)
        {
            if (slot.IsEmpty())
            {
                slot.SetItem(item);
                Notification.Instance.ItemNotification("Отримано новий предмет ", item);
                return true;
            }
        }

        Debug.Log("Нет свободных слотов");
        return false;
    }

    public bool HasItem(ItemData item)
    {
        foreach (var slot in slots)
        {
            if (!slot.IsEmpty() && slot.CurrentItem.id == item.id)
            {
                return true;
            }
        }

        return false;
    }

    public bool RemoveItem(ItemData item)
    {
        foreach (var slot in slots)
        {
            if (slot == null) continue;

            if (slot.CurrentItem == null) continue;

            if (slot.CurrentItem.id == item.id)
            {
                Debug.Log("Удаляю: " + slot.CurrentItem.id);
                slot.Clear();

                CompactItems();
                return true;
            }
        }

        Debug.Log("Предмета нету в инвентаре!!!");
        return false;
    }
    public void CompactItems()
    {
        int targetIndex = 0;

        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].IsEmpty())
            {
                if (i != targetIndex)
                {
                    // переносим предмет
                    var item = slots[i].CurrentItem;

                    slots[targetIndex].SetItem(item);
                    slots[i].Clear();
                }

                targetIndex++;
            }
        }
    }
}
