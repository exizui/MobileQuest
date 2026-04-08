using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventorySlotUI : MonoBehaviour
{
    public Image icon;
    //public Sprite icon;
    [SerializeField] private ItemData currentItem;
    public ItemData CurrentItem => currentItem;
    public bool IsEmpty()
    {
        return currentItem == null;
    }
    private void Awake()
    {
        Clear();
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
}
