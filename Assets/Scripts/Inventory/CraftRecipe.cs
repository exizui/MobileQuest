using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Craft/Recipe")]
public class CraftRecipe : ScriptableObject
{
    public ItemData inputA;
    public ItemData inputB;
    public ItemData inputC;

    public ItemData result;

    public bool isOneTime = true;

    [HideInInspector] public bool used;
}
