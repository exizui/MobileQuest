using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameStateSaveData
{
    public List<string> flags;
}
public class GameState : MonoBehaviour, ISaveable
{
    public static GameState instance { get; private set; }

    private List<string> flags = new List<string>();


    private void Awake()
    {
        instance = this;
    }

    public void SetFlag(string key)
    {
        if (!flags.Contains(key)) // защита от дублей
        {
            flags.Add(key);
            Debug.Log("SET FLAG" +  key);
        }
    }

    public bool HasFlag(string key)
    {
        return flags.Contains(key);
    }

    public bool DeleteFlag(string key)
    {
        return flags.Remove(key);
    }

    public object CaptureState()
    {
        return new GameStateSaveData
        {
            flags = new List<string>(flags),
        };
    }

    public void RestoreState(object state)
    {
        var data = (GameStateSaveData)state;
        flags = data.flags ?? new List<string>();
    }
}