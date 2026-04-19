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
    private bool isResultSlot = true;
    public GameObject craftPanel;
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

    //void OnClick()
    //{
    //    if (currentItem == null) return;

    //    Inventory.instance.AddItem(currentItem, true);
    //    Clear();
    //}
    void OnClick()
    {
        if (currentItem == null) return;

        if (isResultSlot)
        {
            Inventory.instance.AddItem(currentItem);
            OffPanel();
            Clear();
        }
        else
        {
            Clear();
        }
    }

    private void OffPanel()
    {
        craftPanel?.SetActive(false);
    }
}
