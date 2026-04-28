using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseReward : ScriptableObject
{
    [SerializeField] private string rewardName;
    public int amount = 1;
    public abstract void Give();
}
