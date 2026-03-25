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

        UpdateHeader(GetFullDescription());
        OnStart();
    }

    protected abstract void OnStart();

    protected void UpdateHeader(string header)
    {
        QuestUI.instance.ShowHeader(header);
    }
    protected void UpdateUI(string progress ="")
    {
        QuestUI.instance.ShowDescription(progress);
    }

    protected virtual string GetFullDescription()
    {
        return description;
    }
    protected void Complete()
    {
        if(IsCompleted) return;
        IsCompleted = true;
        EndQuest();
    }
    protected abstract void EndQuest();

    

}
