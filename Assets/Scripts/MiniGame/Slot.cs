using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
   public ItemColor slotColor;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;

        DragAndDrop item = eventData.pointerDrag.GetComponent<DragAndDrop>();


        if (item != null && item.itemColor == slotColor)
        {
            item.transform.SetParent(transform);
            item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        }
    }

}
