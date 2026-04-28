using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public ItemColor itemColor;

    public static event System.Action OnAnyDragStart;
    public static event System.Action OnAnyDragEnd;

    private RectTransform rectTransform;
    private Image image;

    private Vector2 startPosition;
    private Transform startParent;

    private DropZone currentPanel;


    //public bool wasDropped = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //wasDropped = true; /// 

        startPosition = rectTransform.anchoredPosition;
        startParent = transform.parent;

        ///
        currentPanel = startParent.GetComponent<DropZone>();
        if (currentPanel != null)
        {
            currentPanel.Remove(rectTransform);
            MiniGameManager.instance.RemoveItem(this);
        }


        transform.SetParent(transform.root);
        ///
        image.raycastTarget = false;

        OnAnyDragStart?.Invoke(); ///
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        //image.color = new Color(1f, 1f, 1f, 1f);
        image.raycastTarget = true;

        var dropZone = eventData.pointerEnter?.GetComponentInParent<DropZone>();
        var slot = eventData.pointerEnter?.GetComponentInParent<Slot>();

        ///old
        //if (transform.parent == startParent)
        //{
        //    rectTransform.anchoredPosition = startPosition;
        //}

        ///new
        //if (transform.parent == transform.root)
        //{
        //    transform.SetParent(startParent);
        //    StartCoroutine(MoveBack(startPosition));
        //}

        ///prev
        //if (dropZone == null && slot == null)
        //{
        //    transform.SetParent(startParent);

        //    // 👉 если это была зона — вернуть в список
        //    var zone = startParent.GetComponent<DropZone>();
        //    if (zone != null)
        //    {
        //        zone.ReturnItem(rectTransform);
        //    }
        //}
        if (transform.parent == transform.root)
        {
            transform.SetParent(startParent);
            StopAllCoroutines();
            StartCoroutine(MoveBack(startPosition));
        }
        OnAnyDragEnd?.Invoke();
    }
    //public void SetDropped()
    //{
    //    wasDropped = true;
    //}
    private IEnumerator MoveBack(Vector2 target)
    {
        float t = 0;
        Vector2 start = rectTransform.anchoredPosition;

        while (t < 0.2f)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(start, target, t / 0.2f);
            t += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = target;
    }
}
