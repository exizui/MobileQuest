
public enum LocationID
{
    Level_Shop,
    Street,
    Level_1,
    Stairs,
    Level_2A, 
    Level_2B, 
    Level_2C,
    Level_3A,  
    Level_3B,
    Level_3C,


    Aud_125,
    Aud_KZ12,
    Aud_138,
    Aud_147,
    Aud_KZ13,
    Aud_152,
    Shop
}

public enum QuestID
{
    Quest_125,
    KZ_12,
    Quest_138,
    Quest_147,
    Quest_FindKeyKZ13,
}

public enum QuestStepType
{
    CollectItem,
    DeliverItem,
    GoToLocation,
    Custom,
    Trigger
}

public enum AnswerActionType
{
    None,
    GiveItem
}

public enum StateLocation
{
    Corridor,
    Audience,
    Street
}