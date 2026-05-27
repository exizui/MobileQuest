using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class CraftSaveData
{
    public string slotA;
    public string slotB;
    public string slotC;
    public string result;
}
public class CraftManager : MonoBehaviour, ISaveable
{
    public CraftSlot slotA;
    public CraftSlot slotB;
    public CraftSlot slotC;

    public List<CraftRecipe> recipes;
    public GameObject craftPanel;
    public CraftSlot resultSlot;

    private void Start()
    {
        foreach (var r in recipes)
            r.used = false;

        //EventManager.instance.TriggerEvent("craft", 3);
        //EventManager.instance.TriggerEvent("craft", 3);
        //EventManager.instance.TriggerEvent("craft", 3);   
    }

    public bool HasItemsInCraft()
    {
        return slotA.currentItem != null ||
               slotB.currentItem != null ||
               slotC.currentItem != null ||
               resultSlot.currentItem != null;
    }
    public void AddItemToCraft(ItemData item)
    {
        Debug.Log($"slotA empty: {slotA.IsEmpty()}, slotB empty: {slotB.IsEmpty()}, slotC empty: {slotC.IsEmpty()}");

        if (slotA.IsEmpty()) 
            slotA.SetItem(item);

        else if 
            (slotB.IsEmpty())  
             slotB.SetItem(item);

        else if (slotC.IsEmpty()) 
            slotC.SetItem(item);

        else
        {
            Debug.Log("Слоты заполнены");
            return;
        }

        Inventory.instance.RemoveItem(item);

        TryCraft();

    }

    public void TryCraft()
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
        Inventory.instance.SetSlotsInteractable(false);
        GameState.instance.DeleteFlag("canCraft");
    }

    public object CaptureState()
    {
        return new CraftSaveData
        {
            slotA = slotA.currentItem != null ? slotA.currentItem.id : null,
            slotB = slotB.currentItem != null ? slotB.currentItem.id : null,
            slotC = slotC.currentItem != null ? slotC.currentItem.id : null,
            //result = resultSlot.currentItem != null ? resultSlot.currentItem.id : null,
        };
    }

    public void RestoreState(object state)
    {
        var data = (CraftSaveData)state;
        RestoreSlot(slotA, data.slotA);
        RestoreSlot(slotB, data.slotB);
        RestoreSlot(slotC, data.slotC);
        RestoreSlot(resultSlot, data.result);

        TryCraft();
        craftPanel.SetActive(false);
    }

    private void RestoreSlot(CraftSlot slot, string itemId)
    {
        if (string.IsNullOrEmpty(itemId))
        {
            slot.Clear();
            return;
        }
        ItemData item = GetItemById(itemId);
        if (item != null) slot.SetItem(item);
    }

    private ItemData GetItemById(string id)
    {
        foreach (var recipe in recipes)
        {
            if (recipe.inputA?.id == id) return recipe.inputA;
            if (recipe.inputB?.id == id) return recipe.inputB;
            if (recipe.inputC?.id == id) return recipe.inputC;
            if (recipe.result?.id == id) return recipe.result;
        }
        return null;
    }
}