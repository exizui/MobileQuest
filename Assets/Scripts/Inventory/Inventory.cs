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
    public InventorySlotUI[] allSlots;
    public event Action<ItemData> OnItemAdded;
    private void Awake()
    {
        instance = this;
    }

    public bool AddItem(ItemData item)
    {
        InventorySlotUI emptySlot = null;

        foreach(var slot in slots)
        {
            if (slot.IsEmpty())
            {
                emptySlot = slot;
                break;
            }
        }

        if(emptySlot == null)
        {
            GameState.instance.SetFlag("FullINV");
            Notification.instance.ItemNotification("Інвентар заповнений", item);
            return false;
        }
        GameState.instance.DeleteFlag("FullINV");
        emptySlot.SetItem(item);
        OnItemAdded?.Invoke(item);
        Notification.instance.ItemNotification("Отримано новий предмет", item);
        return true;
    }

    #region Перегрузка методів додавання предмету без сповіщення 
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
    #endregion
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

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].Clear();

            if (string.IsNullOrEmpty(data.itemIDs[i]))
                continue;

            ItemData item = System.Array.Find(allItems, x => x.id == data.itemIDs[i]);

            if (item != null)
                slots[i].SetItem(item);
        }
    }

    public bool IsFull()
    {
        foreach (var slot in slots)
        {
            if (slot.IsEmpty())
                GameState.instance.DeleteFlag("FullINV");
                return false;
        }
        GameState.instance.SetFlag("FullINV");
        return true;
    }

    #region Захист від переповнення

    public bool RemoveAll(ItemData[] items)
    {
        if (items == null || items.Length == 0)
            return false;
        bool removed = false;
        foreach (var slot in slots)
        {
            if (slot == null || slot.IsEmpty())
                continue;

            foreach (var item in items)
            {
                if (item == null) continue;

                if (slot.CurrentItem.id == item.id)
                {
                    Debug.Log("Delete" + slot.CurrentItem.id);
                    slot.Clear();
                    removed = true;
                    break;
                }
            }
        }

        if (removed)
            CompactItems();

        return removed;
    }

    public bool HasAny(ItemData[] items)
    {
        if(items == null || items.Length == 0) return false;

        foreach (var slot in slots)
        {
            if(slot.IsEmpty()) continue;

            foreach (var item in items)
            {
                if(item == null) continue;

                if(slot.CurrentItem.id == item.id)
                    return true;
            }
        }
        return false;
    }
    #endregion
    public void SetSlotsInteractable(bool state)
    {
        foreach (var slot in allSlots)
        {
            if (slot != null)
                slot.SetInteractable(state);
        }
    }
}
