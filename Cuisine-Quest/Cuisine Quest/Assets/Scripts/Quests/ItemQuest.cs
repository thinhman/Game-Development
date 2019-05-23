using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DungeonFresh/ItemQuest")]
public class ItemQuest : Quest
{
    public AreaScriptable AreaNeeded;

    public override bool CheckCompletion(CiscoTesting player)
    {
        foreach(RequiredItem requiredItem in questData.requiredItems)
        {
            Item item = requiredItem.item.GetComponent<Item>();
            if (item != null && player.items.ContainsKey(item))
            {
                //If the item is an inventory item and the name of the gameobject
                //matches that of the required item for the quest 
                if(item.Type == ItemType.Inventory && item.Name == requiredItem.item.Name)
                {
                    //If one of the items doesnt meet the requirement then the 
                    //quest is not completed
                    if (player.items[requiredItem.item] < requiredItem.requiredAmount)
                    {

                        questData.questState = QuestState.inProgress;
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        //It went through all and were greater than or equal to the amount
        //required to set to complete and return true
        questData.questState = QuestState.completed;
        return true;
    }
}
