using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DropZone: MonoBehaviour
{
    public ItemColor acceptedColor;

    private void OnTriggerEnter2D(Collider2D other)
    {
        GetComponent<BoxCollider2D>().isTrigger=true;
        if (!other.TryGetComponent(out WorldItem item)) return;

        // защита от повторных срабатываний
        other.enabled = false;

        if (item.color == acceptedColor)
        {
            item.SetCorrect(transform);
            MiniGameManager.instance.Correct();
        }
        else
        {
            //item.ReturnToStart();
            MiniGameManager.instance.Wrong();
        }
    }
}
