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
    //[SerializeField] private string TriggerID;
    public ItemData key;

    public Button next;
    public Button prev;

    private void Awake()
    {
        door = GetComponent<Door>();
        button = GetComponent<Button>();
    }
    public void TryOpenDoor()
    {
        if (Inventory.instance.HasItem(key)){
            door.OpenDoor();
            Inventory.instance.RemoveItem(key);
            QuestManager.instance.ItemDelivered(key);

            AddOpenDoorListener();
            Destroy(this);
        }
        else
        {
            Notification.instance.ShowMessage("У ВАС НЕМАЄ КЛЮЧА !!!");
            OffButton();
            //QuestManager.instance.Trigger(TriggerID);
        }

    }

    public void AddOpenDoorListener()
    {
        if (button == null) return;
        Debug.Log("добавили новую логику");
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(door.OpenDoor);

    }

    public void OffButton()
    {
        if (next) next.interactable = false;
        if (prev) prev.interactable = false;
        button.enabled = false;
        OnButton();
    }

    public void OnButton()
    {
        if (next) next.interactable = true;
        if (prev) prev.interactable = true;
        button.enabled = true;
    }
}
