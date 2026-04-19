using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Reward", menuName = "Quests/Rewards/Item")]
public class ItemReward : BaseReward
{
    public ItemData item;
    public override void Give()
    {
        for (int i = 0; i< amount; i++)
        {
            Inventory.instance.AddItem(item);
        }
    }
}
