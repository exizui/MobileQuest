using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpriteChanger : MonoBehaviour, IInteractable
{
    protected SpriteRenderer targetRenderer;

    protected Level_Street lvl;
    protected virtual void Awake()
    {
        targetRenderer = GetComponentInParent<SpriteRenderer>();
        lvl = FindObjectOfType<Level_Street>();
    }

    public virtual void Interact()
    {
        ChangeSprite();
        OnAfterInteract();
        //Destroy(gameObject);
    }

    protected virtual void OnAfterInteract() {  }

    protected virtual void ChangeSprite() { }
}
