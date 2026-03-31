using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static QuestUI; 
public abstract class Quest : MonoBehaviour 
{
    public QuestID QuestID;
    protected QuestUI questUI;
    protected BaseLocations location;

    [SerializeField][TextArea] private string description;
    public bool IsStarted { get; private set; }
    public bool IsCompleted { get; private set; }



    public virtual void Init(BaseLocations location)
    {
        this.location = location;
        questUI = FindObjectOfType<QuestUI>();
    }
    public virtual void StartQuest()
    {
        var audience = location.GetComponent<QuestAudience>();

        if (QuestProgress.Instance.IsCompleted(audience.audienceID, QuestID))
            return;

        if (IsStarted) return;

        IsStarted = true;

        UpdateHeader(GetFullDescription());
        OnStart();
    }

    protected abstract void OnStart();

    protected void UpdateHeader(string header)
    {
        questUI.ShowHeader(header);
    }
    protected void UpdateUI(string progress ="")
    {
        questUI.ShowDescription(progress);
    }

    protected virtual string GetFullDescription()
    {
        return description;
    }
    protected void Complete()
    {
        var audience = location.GetComponent<QuestAudience>();

        if (audience != null)
        {
            Debug.LogError("квест аудиенсе не найден в обьекте");
        }

        if (QuestProgress.Instance.IsCompleted(audience.audienceID, QuestID))
            return;

        QuestProgress.Instance.SetCompleted(audience.audienceID, QuestID);

        IsCompleted = true;
        EndQuest();
    }
    protected abstract void EndQuest();

}
