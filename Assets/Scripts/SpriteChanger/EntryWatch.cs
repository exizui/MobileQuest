using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class EntryWatch : SpriteChanger
{
    [SerializeField] private Sprite newSprite;
    private Level_1 level;
    protected override void Awake()
    {
        base.Awake();
        level = FindObjectOfType<Level_1>();
        //backButton = GetComponentInParent<GameObject>();
    }

    protected override void ChangeSprite()
    {
        if (targetRenderer != null && newSprite != null)
            targetRenderer.sprite = newSprite;
    }

    protected override void OnAfterInteract()
    {
        if(level != null)
        {
            level.ALlowDialogue();
        }
        Destroy(gameObject);
    }
}


