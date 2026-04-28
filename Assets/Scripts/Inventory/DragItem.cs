using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public ItemData item;
    private Transform originalParent;
    private Canvas canvas;
    private Image image;
    private bool wasDropped = false;
    private CanvasGroup canvasGroup;
    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        image = GetComponent<Image>();

        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }
    public void Init(ItemData newItem)
    {
        item = newItem;
        image.sprite = item.icon;
        Debug.Log("DragItem получил: " + item);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        transform.SetParent(canvas.transform);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        if (!wasDropped)
        {
            transform.SetParent(originalParent);
            transform.localPosition = Vector3.zero;
        }

    }

    public void SetDropped()
    {
        wasDropped = true;
    }
}
