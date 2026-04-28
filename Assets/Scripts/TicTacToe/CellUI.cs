using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CellUI : MonoBehaviour
{
    public int index;
    private Image image;
    private Button button;

    private Sprite xSprite;
    private Sprite oSprite;
    private void Awake()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        button.onClick.AddListener(OnClick);
    }
    public void InitSprites(Sprite x, Sprite o)
    {
        xSprite = x;
        oSprite = o;
    }
    public void OnClick()
    {
        if (!GameManager.instance.IsPlayerTurn) return;

        if (GameManager.instance.game.PlayerMove(index))
        {
            GameManager.instance.OnPlayerMoved(); 
        }

        print("qewqwedqw");
    }

    public void SetSymbol(Cell cell)
    {
        if(cell == Cell.X)
        {
            image.sprite = xSprite;
            image.color = Color.white;
        }
        else if(cell == Cell.O)
        {
            image.sprite = oSprite;
            image.color = Color.white;
        }
        else
        {
            //image.enabled = false;
            image.sprite = null;
            image.color = new Color(1, 1, 1, 0);
        }
    }

    public void SetInteractable(bool state)
    {
        button.interactable = state;
    }
}
