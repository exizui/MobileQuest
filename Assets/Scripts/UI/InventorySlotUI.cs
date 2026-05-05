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

    private CraftManager craftManager;

    private Button button;
    public bool IsEmpty()
    {
        return currentItem == null;
    }
    private void Awake()
    {
        button = GetComponent<Button>();
        craftManager = FindObjectOfType<CraftManager>();

        button.onClick.AddListener(OnClick);
        button.interactable = false;

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
    public void SetInteractable(bool state)
    {
        button.interactable = state;
    }
    public void AddOnClick()
    {
        button.onClick.AddListener(OnClick);
    }
    private void OnClick()
    {
        if (currentItem != null)
        {
            craftManager.AddItemToCraft(currentItem);
        }
    }
}
