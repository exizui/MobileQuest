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

    //private const string SAVE_KEY = "GAME_FLAGS";
    [SerializeField] private string id = "game_state";

    private List<string> flags = new List<string>();

    private void Awake()
    {
        instance = this;
        //Load();
    }

    public void SetFlag(string key)
    {
        if (!flags.Contains(key)) // защита от дублей
        {
            flags.Add(key);
            Debug.Log("SET FLAG" +  key);
            //Save();
        }
    }

    public bool HasFlag(string key)
    {
        return flags.Contains(key);
    }

    public void DeleteFlag(string key)
    {
        if (flags.Remove(key))
        {
           // Save();
        }
    }

    public string GetID()
    {
        return id;
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
    #region Old save/load
    //private void Save()
    //{
    //    string json = JsonUtility.ToJson(new Wrapper(flags));
    //    PlayerPrefs.SetString(SAVE_KEY, json);
    //    PlayerPrefs.Save();
    //}

    //private void Load()
    //{
    //    if (!PlayerPrefs.HasKey(SAVE_KEY)) return;

    //    string json = PlayerPrefs.GetString(SAVE_KEY);
    //    flags = JsonUtility.FromJson<Wrapper>(json).flags;
    //}

    //[System.Serializable]
    //private class Wrapper
    //{
    //    public List<string> flags;
    //    public Wrapper(List<string> flags)
    //    {
    //        this.flags = flags;
    //    }
    //}
    #endregion
}