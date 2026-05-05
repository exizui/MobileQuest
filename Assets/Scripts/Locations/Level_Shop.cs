using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Shop : Locations
{
    public ItemData trueCoffee;
    public ItemData emptyItem;
    public GameObject shopDoor;
    public override void Entry()
    {
        base.Entry();
        Check();
    }
    private void Check()
    {
        bool isTrueCoffee = Inventory.instance.HasItem(trueCoffee);

        if (isTrueCoffee)
        {
            GameState.instance.DeleteFlag("buyCoffee");
            shopDoor.SetActive(false);
        }

        InventorySecurity.instance.CheckInv();

    }
    public override void Exit()
    {
        base.Exit();
    }
}
