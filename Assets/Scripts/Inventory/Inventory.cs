using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventorySaveData
{
    public List<string> itemIDs = new List<string>();
}
public class Inventory : MonoBehaviour, ISaveable
{
    public InventorySlotUI[] slots = new InventorySlotUI[5];

    public static Inventory instance;
    public ItemData[] allItems;
    public event Action<ItemData> OnItemAdded;
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
                OnItemAdded?.Invoke(item);
                Notification.instance.ItemNotification("Отримано новий предмет ", item);
                return true;
            }
        }

        Debug.Log("Нет свободных слотов");
        return false;
    }

    public bool AddItem(ItemData item, bool isSilent)
    {
        if (isSilent)
        {
            foreach (var slot in slots)
            {
                if (slot.IsEmpty())
                {
                    slot.SetItem(item);
                    OnItemAdded?.Invoke(item);
                    return true;
                }
            }
        }
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


    public object CaptureState()
    {
        InventorySaveData data = new InventorySaveData();

        foreach (var slot in slots)
        {
            if (slot.IsEmpty())
                data.itemIDs.Add("");
            else
                data.itemIDs.Add(slot.CurrentItem.id);
        }

        return data;
    }

    public void RestoreState(object state)
    {
        var data = (InventorySaveData)state;

        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].Clear();

            if (string.IsNullOrEmpty(data.itemIDs[i]))
                continue;

            ItemData item = System.Array.Find(allItems, x => x.id == data.itemIDs[i]);

            if(item != null)
                slots[i].SetItem(item);
        }
    }
}
