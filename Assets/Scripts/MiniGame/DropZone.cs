using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DropZone: MonoBehaviour, IDropHandler
{
    public ItemColor panelColor;
    public float spacing = 150f;
    private const int MAX_ITEMS = 6;

    private List<RectTransform> items = new List<RectTransform>();

    private Image image;
    private Color defaultColor;

    [Range(0f, 1f)]
    private float highlightAlpha = 0.8f;

    private void Awake()
    {
        image = GetComponent<Image>();
        defaultColor = image.color;
    }
    private void OnEnable()
    {
        DragAndDrop.OnAnyDragStart += Highlight;
        DragAndDrop.OnAnyDragEnd += ResetColor;
    }

    private void OnDisable()
    {
        DragAndDrop.OnAnyDragStart -= Highlight;
        DragAndDrop.OnAnyDragEnd -= ResetColor;
    }
    void Start()
    {
        items.Clear();

        foreach (Transform child in transform)
        {
            RectTransform rect = child as RectTransform;

            if (rect != null)
            {
                ///
                //rect.SetParent(transform, false); // <-- ВАЖНО
                //rect.anchoredPosition = Vector2.zero;
                //rect.localScale = Vector3.one;
                ///
                items.Add(rect);
            }
              
        }

        Rearrange();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null) return;
        DragAndDrop item = eventData.pointerDrag.GetComponent<DragAndDrop>();
        //if (item == null || item.itemColor != panelColor) return;

        RectTransform rect = item.GetComponent<RectTransform>();

        if (!items.Contains(rect) && items.Count >= MAX_ITEMS)
        {
            Debug.Log("Лимит достигнут");
            return; // ❌ ничего не делаем
        }
        rect.SetParent(transform);
        //rect.anchoredPosition = Vector2.zero;
        if (!items.Contains(rect))
            items.Add(rect);
        Debug.Log($"{gameObject.name} → {items.Count}/{MAX_ITEMS}");
        //item.SetDropped();
        Rearrange();
        MiniGameManager.instance.EvaluateItem(item, this);
    }

    public void Remove(RectTransform rect)
    {
        if (items.Contains(rect))
        {
            items.Remove(rect);
            Debug.Log($"REMOVE{gameObject.name} → {items.Count}/{MAX_ITEMS}");
            Rearrange();
        }
    }

    private void Rearrange()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Vector2 target = new Vector2(i * spacing, 0);
            StartCoroutine(MoveTo(items[i], target));
        }
    }

    private IEnumerator MoveTo(RectTransform rect, Vector2 target)
    {
        float t = 0;
        Vector2 start = rect.anchoredPosition;

        while (t < 0.2f)
        {
            rect.anchoredPosition = Vector2.Lerp(start, target, t / 0.2f);
            t += Time.deltaTime;
            yield return null;
        }

        rect.anchoredPosition = target;
    }

    public void ReturnItem(RectTransform rect)
    {
        //if (!items.Contains(rect))
        //{
        //    items.Add(rect);
        //}
        Rearrange();
    }

    private void Highlight()
    {
        Color color = defaultColor * 0.8f; // 👈 затемнение (множитель)
        color.a = highlightAlpha;
        image.color = color;
    }

    private void ResetColor()
    {
        image.color = defaultColor;
    }

}
