using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryObj;
    public GameObject craftObj;
    public Button button;
    public static event Action OnCraft;
    public CraftManager craftManager;
    private void Start()
    {
        inventoryObj.SetActive(false);
        ClearButton();
        button.onClick.AddListener(OpenInventory);
    }

    public void OpenInventory()
    {
        inventoryObj.SetActive(true);

        if (GameState.instance.HasFlag("canCraft"))
        {
            OnCraft?.Invoke();
            craftObj.SetActive(true);
            Inventory.instance.SetSlotsInteractable(true);
        }
        else
        {
            Inventory.instance.SetSlotsInteractable(false);
        }

        ClearButton();
        button.onClick.AddListener(CloseInventory);
    }

    public void CloseInventory()
    {
        if (craftObj.activeSelf && craftManager.HasItemsInCraft())
        {
            Notification.instance.ShowMessage("Забери предмет з крафту");
            return;
        }

        inventoryObj.SetActive(false);
        craftObj.SetActive(false);

        Inventory.instance.SetSlotsInteractable(false);

        ClearButton();
        button.onClick.AddListener(OpenInventory);
    }
   

    private void ClearButton()
    {
        button.onClick.RemoveAllListeners();
    }
}
