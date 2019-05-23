using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests;

    /// <summary>
    /// Makes this class non-destroyable.
    /// </summary>
    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Initializes the quest scriptable objects.
    /// </summary>
    /// <param name="playerData">Player Quest data from the JSON file.</param>
    public void InitQuestScriptableObjects(PlayerQuestArray playerData)
    {
        int index = 0;
        foreach(var quest in playerData.items)
        {
            quests[index].questData.questState = quest.questState;
            quests[index].questID = quest.questID;
            index++;
        }
    }

    public List<Quest> GetQuests()
    {
        return quests;
    }

    public bool finishedEveryQuest()
    {
        foreach(Quest quest in quests)
        {
            if(quest.questData.questState != QuestState.done)
            {
                return false;
            }
        }
        return true;
    }
}
