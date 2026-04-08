using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TriggerWatchman : MonoBehaviour
{
    [SerializeField] private Button button;
    public static bool isOpen;
    [SerializeField] private SpriteRenderer targetRenderer;
    [SerializeField] private Sprite newSprite;

    [SerializeField] private TextMeshProUGUI buttTxt;
    private Level_1 level;
    private void Awake()
    {
        level = GetComponentInParent<Level_1>();
    }

    public void OnCLick()
    {
        if (targetRenderer != null && newSprite != null)
        {
           targetRenderer.sprite = newSprite;
           level.ALlowDialogue();

        }
    }

}
