using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitWatch : MonoBehaviour
{
    private SpriteRenderer targetRenderer;
    [SerializeField] private Sprite oldSprite;
    private Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        targetRenderer = GetComponentInParent<SpriteRenderer>();
    }
    public void OnClick()
    {
        if (targetRenderer != null && oldSprite != null)
        {
            targetRenderer.sprite = oldSprite;
            Destroy(gameObject);
        }
    }
      
}
