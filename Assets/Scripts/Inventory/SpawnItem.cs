using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    public ItemData[] itemDatas;
    //private void Start()
    //{
    //    Spawn();
    //}

    public void Spawn()
    {
        foreach (ItemData item in itemDatas)
        {
            Inventory.instance.AddItem(item);
        }
    }
}
