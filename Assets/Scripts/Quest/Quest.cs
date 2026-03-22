using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static QuestUI; 
public abstract class Quest : MonoBehaviour 
{
    protected BaseLocations location;
    [SerializeField][TextArea] private string description;
    public bool IsStarted { get; private set; }
    public bool IsCompleted { get; private set; }
    public virtual void Init(BaseLocations location)
    {
        this.location = location;
    }
    public virtual void StartQuest()
    {
        if (IsStarted) return;

        IsStarted = true;

        UpdateUI();
        OnStart();
    }

    protected abstract void OnStart();
    protected void UpdateUI()
    {
        instance.Show(GetFullDescription());
    }

    protected virtual string GetFullDescription()
    {
        return description;
    }
    protected void Complete()
    {
        if(!IsCompleted) return;
        IsCompleted = true;
        EndQuest();
    }
    public virtual void EndQuest() { }

}
