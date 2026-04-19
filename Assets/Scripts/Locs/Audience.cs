using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static QuestManager;
public class Audience : BaseLocations
{
    protected QuestUI questUI;
    protected QuestManager questManager;
    //protected QuestTest questTest;

    //public AudienceID audienceID;

    [SerializeField] protected List<QuestID> quests;
    public List<QuestID> Quests => quests;
    private void Awake()
    {
        questUI = FindObjectOfType<QuestUI>();
        questManager = FindObjectOfType<QuestManager>();
        //questTest = FindObjectOfType<QuestTest>();
    }
    //public override void Entry()
    //{
    //    base.Entry();
        
    //}

    //public override void Exit()
    //{
    //    //Controller.SetPrevLocation(id);
    //    base.Exit();
    //}
}
