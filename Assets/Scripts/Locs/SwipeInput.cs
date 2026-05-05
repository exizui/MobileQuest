using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeInput : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 endPos;

    [SerializeField] private float minSwipeDistance = 100f;

    private void Update()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                startPos = touch.position;
                break;
            case TouchPhase.Ended:
                endPos = touch.position;
                HandleSwipe();
                break;
        }
    }

    private void HandleSwipe()
    {
        if (!LocationNavigator.Controller.IsSwipeEnabled())
            return;

        float deltaX = endPos.x - startPos.x;
        float deltaY = endPos.y - startPos.y;

        if (Mathf.Abs(deltaX) < Mathf.Abs(deltaY))
            return;

        if (Math.Abs(deltaX) < minSwipeDistance)
            return;

        if(deltaX > 0)
        {
            LocationNavigator.Controller.PrevLocation();
        }
        else
        {
            LocationNavigator.Controller.NextLocation();
        }
    }
}
