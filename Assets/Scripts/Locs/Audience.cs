using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static QuestManager;
public abstract class Audience : BaseLocations
{
    protected QuestUI questUI;
    protected QuestManager questManager;
    protected QuestTest questTest;
    private void Awake()
    {
        questUI = FindObjectOfType<QuestUI>();
        questManager = FindObjectOfType<QuestManager>();
        questTest = FindObjectOfType<QuestTest>();
    }
    public override void Entry()
    {
        base.Entry();
        
    }

    public override void Exit()
    {
        //Controller.SetPrevLocation(id);
        base.Exit();
    }
}
