using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGameMenu : MonoBehaviour 
{
    public QuestUI questUI;

    public static bool paused;

    public void SaveGame()
    {
        paused = true;
        questUI.showQuestUI = false;
        AudioSourceController.Instance.PlayAudio("Save");
        SaveSystem.Instance.SaveGame();
        Cancel();
    }

    public void Cancel()
    {
        paused = false;
        gameObject.SetActive(false);
    }
}
