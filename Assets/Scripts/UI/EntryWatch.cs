using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EntryWatch : MonoBehaviour
{
    private SpriteRenderer targetRenderer;
    [SerializeField] private Sprite newSprite;
    private Level_1 level;
    private void Awake()
    {
        level = FindObjectOfType<Level_1>();
        targetRenderer = GetComponentInParent<SpriteRenderer>();
    }
    public void OnCLick()
    {
        if (targetRenderer != null && newSprite != null)
        {
            targetRenderer.sprite = newSprite;
            level.ALlowDialogue();
            Destroy(gameObject);

        }
    }
}
