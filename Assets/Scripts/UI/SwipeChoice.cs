using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class SwipeChoice : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform swipeArea;
    [SerializeField] private RectTransform handle;

    private Vector2 startPos;

    private float minX, maxX;
    private float minY, maxY;

    private float threshold = 20f;

    private DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();

        startPos = handle.anchoredPosition;

        float areaWidth = swipeArea.rect.width;
        float areaHeight = swipeArea.rect.height;

        float handleWidth = handle.rect.width;
        float handleHeight = handle.rect.height;

        // границы по X
        minX = -areaWidth / 2f + handleWidth / 2f;
        maxX = areaWidth / 2f - handleWidth / 2f;

        // границы по Y
        minY = -areaHeight / 2f + handleHeight / 2f;
        maxY = areaHeight / 2f - handleHeight / 2f;
    }

    public void OnBeginDrag(PointerEventData eventData) {  }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 localPoint;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            swipeArea,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint
        );

        Vector2 newPos = localPoint;

        // ограничение внутри панели
        newPos.x = Mathf.Clamp(newPos.x, minX, maxX);
        newPos.y = Mathf.Clamp(newPos.y, minY, maxY);

        handle.anchoredPosition = newPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // возврат в центр

        if (Mathf.Abs(handle.anchoredPosition.x - startPos.x) > threshold)
        {
            if (handle.anchoredPosition.x > startPos.x)
            {
                dialogueManager.GetAnswerButton(1);
            }
            else
            {
                dialogueManager.GetAnswerButton(0);
            }
        }
        else if (Mathf.Abs(handle.anchoredPosition.y - startPos.y) > threshold)
        {
            if (handle.anchoredPosition.y > startPos.y)
            {
                dialogueManager.GetAnswerButton(2);
            }
            else
            {
                dialogueManager.GetAnswerButton(3);
            }
        }
        handle.anchoredPosition = startPos;
    }
}