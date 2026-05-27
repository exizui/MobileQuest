using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CraftSlot : MonoBehaviour
{
    public ItemData currentItem;
    public Image icon;
    private Button button;

    public CraftManager craftManager;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }
    public bool IsEmpty()
    {
        return currentItem == null;
    }

    public void SetItem(ItemData item)
    {
        currentItem = item;
        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void Clear()
    {
        currentItem = null;
        icon.sprite = null;
        icon.enabled = false;
    }

    void OnClick()
    {
        if (currentItem == null) return;

        if (Inventory.instance.IsFull())
        {
            Debug.Log("Інвентар повний");
            return;
        }

        Inventory.instance.AddItem(currentItem, true);
        Clear();

        //if (this == craftManager.resultSlot)
        //{
        //    craftManager.OffPanel();
        //}
        //craftManager.TryCraft();
    }
    
    public void SetItemFromDrag(ItemData item)
    {
        if (!IsEmpty()) return;

        SetItem(item);

        Inventory.instance.RemoveItem(item);

        craftManager.TryCraft();
    }
}
