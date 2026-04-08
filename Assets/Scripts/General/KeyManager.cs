using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class KeyManager : MonoBehaviour
{
    private LocationNavigator locationNavigator;
    private Door door;
    private Button button;

    public ItemData key;



    private void Awake()
    {
        button = GetComponent<Button>();
        door = GetComponent<Door>();
    }
    public void TryOpenDoor()
    {
        if (Inventory.instance.HasItem(key)){
            door.OpenDoor();
            Inventory.instance.RemoveItem(key);
            AddOpenDoorListener();
        }
        else
        {
            Notification.Instance.ShowMessage("У ВАС НЕМАЄ КЛЮЧА !!!");           
        }

    }

    public void AddOpenDoorListener()
    {
        Debug.Log("добавили новую логику");
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(door.OpenDoor);
        Destroy(this);
    }
}
