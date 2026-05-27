using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickableItem : MonoBehaviour 
{
    public ItemData item;

    private void OnMouseDown()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        ItemData newItem = Instantiate(item);
        newItem.icon = sr.sprite;

        if (Inventory.instance.AddItem(item))
        {
            Destroy(gameObject);
        }
    }
}
