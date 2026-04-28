using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public ItemData[] itemDatas;
    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        Inventory.instance.AddItem(itemDatas[0]);
        Inventory.instance.AddItem(itemDatas[1]);
        Inventory.instance.AddItem(itemDatas[2]);
    }
}
