using UnityEngine;

//[CreateAssetMenu(menuName = "DungeonFresh/Quest")]
public abstract class Quest : ScriptableObject 
{
    public QuestData questData;
    public Quest[] DependentQuests;
    public Item reward;
    public int questID;

    public abstract bool CheckCompletion(CiscoTesting player);
}
