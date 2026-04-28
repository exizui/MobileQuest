using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftManager : MonoBehaviour
{
    public CraftSlot slotA;
    public CraftSlot slotB;
    public CraftSlot slotC;

    public List<CraftRecipe> recipes;
    public Inventory inventory;
    public CraftSlot resultSlot;


    private void Start()
    {
        foreach (var r in recipes)
            r.used = false;
    }
    public void AddItemToCraft(ItemData item)
    {
        if (slotA.IsEmpty()) slotA.SetItem(item);
        else if (slotB.IsEmpty()) slotB.SetItem(item);
        else if (slotC.IsEmpty()) slotC.SetItem(item);
        else
        {
            Debug.Log("Слоты заполнены");
            return;
        }
        Debug.Log(inventory);

        inventory.RemoveItem(item);

        TryCraft();

    }

    void TryCraft()
    {
        if (slotA.currentItem == null ||
            slotB.currentItem == null ||
            slotC.currentItem == null)
            return;

        List<ItemData> inputs = new List<ItemData>
        {
            slotA.currentItem,
            slotB.currentItem,
            slotC.currentItem
        };

        foreach (var recipe in recipes)
        {
            if (recipe.isOneTime && recipe.used)
                continue;

            List<ItemData> needed = new List<ItemData>
            {
                recipe.inputA,
                recipe.inputB,
                recipe.inputC
            };

            if (Match(inputs, needed))
            {
                Craft(recipe);
                return;
            }
        }

        Debug.Log("Нет рецепта");
    }

    bool Match(List<ItemData> a, List<ItemData> b)
    {
        var temp = new List<ItemData>(b);

        foreach (var item in a)
        {
            if (temp.Contains(item))
                temp.Remove(item);
            else
                return false;
        }

        return temp.Count == 0;
    }

    void Craft(CraftRecipe recipe)
    {
        resultSlot.SetItem(recipe.result); ///////

        slotA.Clear();
        slotB.Clear();
        slotC.Clear();

        //inventory.AddItem(recipe.result);

        if (recipe.isOneTime)
            recipe.used = true;

        Debug.Log("Скрафтил: " + recipe.result.id);

        GameState.instance.DeleteFlag("canCraft");
    }
}