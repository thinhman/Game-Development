using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Quest types. There is kill, collect, and kill and collect types of quest
/// available.
/// </summary>
public enum QuestType
{
    Kill,
    Collect,
    KillAndCollect
}

/// <summary>
/// Quest state.
/// </summary>
public enum QuestState
{
    pending,
    inProgress,
    completed,
    done,
}

/// <summary>
/// Quest data. Each quest has a name,description,type,items needed, and a 
/// completion status
/// </summary>
[System.Serializable]
public class QuestData 
{
    public string questName;
    [TextArea]
    public string description;
    public QuestType questType;
    public List<RequiredItem> requiredItems = new List<RequiredItem>();
    public QuestState questState;
}

/// <summary>
/// Player quest data that will be used to write to a json file.
/// </summary>
[System.Serializable]
public class PlayerQuestData
{
    public int questID;
    public string questName;
    public bool hasQuest;
    public QuestState questState;
    public int[] amountDone;
}

/// <summary>
/// Array of playerquestdata in order to read multiple objects from the 
/// same json file
/// </summary>
[System.Serializable]
public class PlayerQuestArray
{
    public PlayerQuestData[] items;
}
