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

    private void Start()
    {
        inventoryObj.SetActive(false);
        ClearButton();
        button.onClick.AddListener(OpenInventory);
    }
    //private void OnEnable()
    //{
    //    SceneLoader.OnLoadScene += Disable;
    //}

    //private void Disable()
    //{
    //    button.gameObject.SetActive(false);
    //    SceneLoader.OnLoadScene -= Disable;
    //    SceneLoader.OnLoadScene += Enable;
    //}

    //private void Enable()
    //{
    //    button.gameObject.SetActive(true);
    //    SceneLoader.OnLoadScene -= Enable;
    //}

    public void OpenInventory()
    {
        inventoryObj.SetActive(true);

        if (GameState.instance.HasFlag("canCraft"))
        {
            craftObj.SetActive(true);
        }

        ClearButton();
        button.onClick.AddListener(CloseInventory);
    }

    public void CloseInventory()
    {
        inventoryObj.SetActive(false);
        craftObj.SetActive(false);
        ClearButton();
        button.onClick.AddListener(OpenInventory);
    }


    private void ClearButton()
    {
        button.onClick.RemoveAllListeners();
    }
}
