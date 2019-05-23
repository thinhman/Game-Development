using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestSystem : MonoBehaviour, ISaveable
{
    QuestManager questManager;
    [SerializeField]
    public List<PlayerQuestData> currentQuests;
    private readonly string fileName = "Quests.json";
    private string filePath;

    // Use this for initialization
    void Start () 
    {
        SaveSystem.Instance.AddSaveableObject(this);
        filePath = Path.Combine(Application.persistentDataPath, fileName);

        questManager = FindObjectOfType<QuestManager>();
        if (!File.Exists(filePath))
        {
            currentQuests = new List<PlayerQuestData>();
            InitQuestData();
        }
        else
        {
            GetQuestsFromFile();
        }
    }
	
    /// <summary>
    /// Gets the quests from the filename specified
    /// </summary>
    private void GetQuestsFromFile()
    {
        PlayerQuestArray quests = JsonArrayHandler<PlayerQuestArray>.ReadJsonFile(filePath);
        foreach(var quest in quests.items)
        {
            currentQuests.Add(quest);
        }
        questManager.InitQuestScriptableObjects(quests);
    }

    /// <summary>
    /// Initializes the quest data as if the player has just started their
    /// journey in the game
    /// </summary>
    private void InitQuestData()
    {
        Debug.Log("Init quest data log");
        PlayerQuestData questData = new PlayerQuestData();
        //For all the quests we have in the manager we make 
        //a clean slate for all of them
        for(int i = 0; i < questManager.quests.Count; i++)
        {
            questData.questID = i;
            questData.questName = questManager.quests[i].questData.questName;
            questData.questState = QuestState.pending;
            questManager.quests[i].questData.questState = questData.questState;
            questData.hasQuest = false;
            int requiredItemCount = questManager.GetQuests()[i].questData.requiredItems.Count;
            questData.amountDone = new int[requiredItemCount];
            for (int j = 0; j < questData.amountDone.Length; j++)
            {
                questData.amountDone[j] = 0;
            }

            currentQuests.Add(questData);
            questData = new PlayerQuestData();
        }
        Save();
    }

    public bool GetHasQuestByID(int id)
    {
        return currentQuests[id].hasQuest;
    }

    public int[] GetQuestCompletionStatus(int id)
    {
        return currentQuests[id].amountDone;
    }

    public Quest GetQuestByID(int id)
    {
        return questManager.quests[id];
    }

    public List<Quest> GetQuests()
    {
        return questManager.GetQuests();
    }

    public void UpdateQuests(int id, Dictionary<Item, int> items, Item item)
    {
        //if player has the item in the inventory
        UpdateItemDisplay(id, items, item);
    }

    public void UpdateCurrentQuestsAmountDone(Dictionary<Item, int> items)
    {
        for (int i = 0; i < currentQuests.Count; i++)
        {
            for (int j = 0; j < questManager.GetQuests()[i].questData.requiredItems.Count; j++)
            {
                if (items.ContainsKey(questManager.GetQuests()[i].questData.requiredItems[j].item))
                {
                    currentQuests[i].amountDone[j] = items[questManager.GetQuests()[i].questData.requiredItems[j].item];
                }
                else
                {
                    currentQuests[i].amountDone[j] = 0;
                }
            }
        }
    }

    public void UpdateItemDisplay(int id, Dictionary<Item, int> items, Item item)
    {
        int num = 0;
        foreach (RequiredItem requiredItem in questManager.GetQuests()[id].questData.requiredItems)
        {
            if (item.Name.Contains(requiredItem.item.Name))
            {
                currentQuests[id].amountDone[num] = items[item];
                return;
            }
            num++;
        }
    }

    public int GetActiveQuestCount()
    {
        int count = 0;
        foreach(PlayerQuestData data in currentQuests)
        {
            if(data.hasQuest)
            {
                count++;
            }
        }
        return count;
    }

    public void SetQuestStatus(int id, QuestState state)
    {
        currentQuests[id].hasQuest = true;
        questManager.quests[id].questData.questState = state;
    }

    public void Save()
    {
        //Update the quest states from the scriptable objects
        for (int i = 0; i < questManager.GetQuests().Count; i++)
        {
            currentQuests[i].questState = questManager.GetQuests()[i].questData.questState;
        }
        JsonArrayHandler<PlayerQuestData>.WriteJsonFile(filePath, currentQuests);
    }
}
