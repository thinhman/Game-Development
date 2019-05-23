using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public List<ISaveable> saveableObjects; // list of scripts with ISaveable interface defined 
    public List<string> fileNames;

    private static SaveSystem instance = null;

    public static SaveSystem Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(this);

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

        saveableObjects = new List<ISaveable>();
    }

    /// <summary>
    /// Saves the game going thru the list of saveable objects and calling 
    /// the save function which will return the json format to the object
    /// to write to the file.Once the file is saved the writer will close
    /// thus flushing the buffer into the file correctly.
    /// </summary>
    public void SaveGame()
    {
        Debug.Log("Game Saved");
        AudioSourceController.Instance.StartCoroutine(AudioSourceController.Instance.PlaySaveMusic());
        foreach (ISaveable saveObject in saveableObjects)
        {
            saveObject.Save();
        }
    }

    public void NewGame()
    {
        Debug.Log("New Game");
        foreach (string fileName in fileNames)
        {
            File.Delete(Path.Combine(Application.persistentDataPath, fileName));
        }
    }

    public void AddSaveableObject(ISaveable objectToSave)
    {
        saveableObjects.Add(objectToSave);
    }
}
