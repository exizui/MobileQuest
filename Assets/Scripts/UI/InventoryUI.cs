using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryObj;
    public Button button;

    private void Start()
    {
        inventoryObj.SetActive(false);
        ClearButton();
        button.onClick.AddListener(OpenInventory);
    }

    public void OpenInventory()
    {
        inventoryObj.SetActive(true);
        ClearButton();
        button.onClick.AddListener(CloseInventory);
    }

    public void CloseInventory()
    {
        inventoryObj.SetActive(false);
        ClearButton();
        button.onClick.AddListener(OpenInventory);
    }


    private void ClearButton()
    {
        button.onClick.RemoveAllListeners();
    }
}
