using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickableItem : MonoBehaviour //IPointerClickHandler
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




    #region Нажатие через онпоинтер, нужно интерфейс подключить 
    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    if (inventory.AddItem(item))
    //    {
    //        Destroy(gameObject);
    //    }
    //}
    #endregion

}
