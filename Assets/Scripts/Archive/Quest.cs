using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

/*
public abstract class Quest : MonoBehaviour
{
    [SerializeField] private UnityEvent onComplete;
    private bool active;
    private QuestManager manager;

    public virtual void Init(QuestManager m)
    {
        manager = m;
    }

    public abstract string GetHeader();

    public abstract string GetDesctiption();

    public virtual void Activate()
    {
        active = true;
    }

    public virtual void Notification()
    {
        throw new System.Exception("начался квест");
    }
    protected void Complete()
    {
        if (!active)
        {
            return;
        }
        onComplete.Invoke();
        active = false;
        manager.NextQuest();
    }
}
*/