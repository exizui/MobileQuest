using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardUI : MonoBehaviour
{
    public CellUI[] cells;
    public GameBot game;

    public void InitSprites(Sprite x, Sprite o)
    {
        foreach (var c in cells)
        {
            c.InitSprites(x, o);
        }
    }
    public void Refresh()
    {
        for(int i=0; i < cells.Length; i++)
        {
            var cellState = game.GetCell(i);

            cells[i].SetSymbol(cellState);

            cells[i].SetInteractable(cellState == Cell.Empty);
        }
    }

    public void SetAllInteractable(bool state)
    {
        foreach(var c in cells)
            c.SetInteractable(state);
    }

   
}
