using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitWatch : SpriteChanger
{
    [SerializeField] private Sprite oldSprite;

    protected override void ChangeSprite()
    {
        if (targetRenderer != null && oldSprite != null)
            targetRenderer.sprite = oldSprite;
        Destroy(gameObject);
    }
}

