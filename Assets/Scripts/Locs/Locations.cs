using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LocationNavigator;
#region OLD LOCATIONS
//public class Locations : BaseLocations, IQuestHolder
//{
//    public LocationID id;

//    [Header("Навігація")]
//    public LocationID? next;
//    public LocationID? prev;
//    public LocationID? stairs;

//    [Header("Quests")]
//    [SerializeField] protected List<QuestID> quests;
//    public List<QuestID> Quests => quests;
//    public string HolderID => "LOC_" + id.ToString();

//    [Header("Optional")]
//    protected DialogueTrigger dialogue;

//    protected QuestUI questUI;

//    private void Awake()
//    {
//        questUI = FindObjectOfType<QuestUI>();

//        if (dialogue == null)
//            dialogue = GetComponent<DialogueTrigger>();
//    }
//    public string HolderId => "LOC_" + id.ToString();

//    //protected override void OnEnter() ///ЗАКОМЕНТИРОВАНО
//    //{
//    //    if (dialogue != null)
//    //    {
//    //        dialogue.TriggerDialogue(OnDialogueEnd);
//    //    }
//    //    else
//    //    {
//    //        StartQuestFlow();
//    //    }
//    //}

//    protected void StartQuestFlow()
//    {
//        bool started = QuestManager.instance.OnLocationEntered(this);

//        if (started)
//        {
//            questUI.ActiveUI();
//        }
//    }

//    private void OnDialogueEnd()
//    {
//        StartQuestFlow();
//    }

//    //protected override void OnExit() //ЗАКОМЕНТИРОВАНО
//    //{
//    //    if (questUI != null)
//    //        questUI.DisActiveUI();
//    //}
//}
#endregion

public class Locations : BaseLocations
{
    public LocationID id;

    [Header("Навігація")]
    public LocationID next;
    public LocationID prev;

    [Header("Optional")]
    protected DialogueTrigger dialogueTrigger;
    protected QuestUI questUI;


    public virtual ILocationState GetState() { return new CorridorState(); }
    private void Awake()
    {
        questUI = FindObjectOfType<QuestUI>();

        if (dialogueTrigger == null)
            dialogueTrigger = GetComponent<DialogueTrigger>();
    }

    public override void Entry()
    {
        base.Entry();

        LocationEvents.OnLocationEntered?.Invoke(this);

        OnEnter();
    }

    //protected override void OnEnter()
    //{
    //    if (dialogue != null)
    //    {
    //        dialogue.TriggerDialogue(OnDialogueEnd);
    //    }
    //}

    private void OnDialogueEnd()
    {
        if (questUI != null)
            questUI.ActiveUI();
    }

    //public override void Exit()
    //{
    //    base.Exit();

    //    if (questUI != null)
    //        questUI.DisActiveUI();
    //}
}