using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestAudience : BaseLocations
{
    public AudienceID audienceID;

    [SerializeField] protected List<QuestID> quests;
    public List<QuestID> Quests => quests;

    protected QuestUI questUI;

    private void Awake()
    {
        questUI = FindObjectOfType<QuestUI>();
    }

    public override void Entry()
    {
        base.Entry();
    }
    public override void Exit()
    {
        base.Exit();
    }

    protected override void OnEnter()
    {
        TryStartQuest();
    }

    private void TryStartQuest()
    {
        foreach (var questID in quests)
        {
            if (!QuestProgress.Instance.IsCompleted(audienceID, questID))
            {
                StartQuestFlow(questID);
                break;
            }
        }
    }

    protected abstract void StartQuestFlow(QuestID questID);
}
