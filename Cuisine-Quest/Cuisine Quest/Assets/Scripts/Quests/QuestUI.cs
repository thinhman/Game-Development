using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour 
{
    public RawImage uiImage;
    QuestManager questManager;
    PlayerQuestSystem playerQuestSystem;
    public GUIStyle boxStyle;
    public GUIStyle questLabels;
    public GUIStyle labelText;
    public GUIStyle questProgressStyle;
    public bool showQuestUI;

    public int offset = 20;
    public int padding = 5;
    public int bottomScreenDisplacement = 145; //Nice number and part of it comes from how long the hearts on the screen is
    readonly Vector2 GUI_BOX_SIZE = new Vector2(300, 50);

    private void Awake()
    {
        questManager = FindObjectOfType<QuestManager>();
        playerQuestSystem = FindObjectOfType<PlayerQuestSystem>();

        labelText.normal.textColor = Color.black;
        questProgressStyle.normal.textColor = Color.white;
        questProgressStyle.fontSize = 25;
        questProgressStyle.contentOffset = new Vector2(0,-5.0f);
        questProgressStyle.alignment = TextAnchor.MiddleLeft;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && !PauseMenu.paused)
        {
            showQuestUI = !showQuestUI;
        }
    }

    private void OnGUI()
    {
        if(showQuestUI && !PauseMenu.paused && !SaveGameMenu.paused)
        {
            //GUI.matrix = matrix;
            //Used to be 250 for width of the boxes
            List<Quest> quests = questManager.GetQuests();
            //Draw Big Box to hold all the quests
            GUI.Box(new Rect(10,uiImage.rectTransform.rect.height + 25,350,Screen.height - bottomScreenDisplacement),"Quests", boxStyle);
            int placeX = 25;
            int placeY = (int)uiImage.rectTransform.rect.height + 55;
            foreach (Quest quest in quests)
            {
                if (playerQuestSystem.GetHasQuestByID(quest.questID) &&
                    (quest.questData.questState == QuestState.inProgress ||
                     (quest.questData.questState == QuestState.completed)))
                {
                    //Draw the name of the quest
                    GUI.Label(new Rect(placeX, placeY, 100, 20), quest.questData.questName,labelText);
                    //Move the placement down by the offset indication
                    placeY += offset;

                    //Check if the quest is completed or not in order to say 
                    //to collect more or just display COMPLETED! This box is scaled
                    //with the essence of multiple quests in mind so itll fit
                    //all the quests inside its own box. 1 Box per quest
                    if (quest.questData.questState == QuestState.completed)
                    {
                        GUI.Box(new Rect(placeX, placeY, GUI_BOX_SIZE.x, GUI_BOX_SIZE.y), "",questLabels);
                        //Just display that the quest has been completed
                        GUI.Label(new Rect(placeX + padding + 5, placeY + padding, GUI_BOX_SIZE.x, GUI_BOX_SIZE.y), "Completed Quest!", questProgressStyle);
                        placeY += (int)GUI_BOX_SIZE.y;
                    }
                    else if(quest.questData.questState == QuestState.inProgress)
                    {
                        GUI.Box(new Rect(placeX, placeY, GUI_BOX_SIZE.x, GUI_BOX_SIZE.y * quest.questData.requiredItems.Count), "", questLabels);
                        //Draw the numbers of stuff left for the quest
                        int index = 0;
                        foreach (RequiredItem requiredItem in quest.questData.requiredItems)
                        {
                            string completionString = string.Format("{0}: ", requiredItem.item.Name);
                            completionString += playerQuestSystem.GetQuestCompletionStatus(quest.questID)[index].ToString();
                            completionString += "/" + requiredItem.requiredAmount;
                            GUI.Label(new Rect(placeX + padding + 5, placeY + padding, GUI_BOX_SIZE.x, GUI_BOX_SIZE.y), completionString, questProgressStyle);
                            //Move the placement down by the offset indication
                            placeY += offset + (padding * 2);
                            index++;
                        }
                    }

                    //Move the placement down by the offset indication
                    placeY += offset + (padding * 2);
                }
            }
        }
    }
}
