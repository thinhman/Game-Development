using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class JsonArrayHandler<Type>
{
    /// <summary>
    /// Reads the json file with the type specified and 
    /// returns the Type of object back to the user.
    /// </summary>
    /// <returns>The json object read from the file.</returns>
    /// <param name="path">Path to the json file.</param>
    public static Type ReadJsonFile(string path)
    {
        StreamReader fileReader = new StreamReader(path);

        Type item = JsonUtility.FromJson<Type>(fileReader.ReadToEnd());

        fileReader.Close();

        return item;
    }

    /// <summary>
    /// Writes the json file using the path specified. It also writes
    /// the json object to the type of object named items.
    /// </summary>
    /// <param name="path">Path to the json file.</param>
    /// <param name="data">Data to be jsonified.</param>
    public static void WriteJsonFile(string path, List<Type> data)
    {
        StreamWriter fileWriter = new StreamWriter(path);

        //TODO::change it to so we can specify the object name
        //or keep it generic so all need to have items as the name of the array
        string jsonString = "{ \"items\": [\n";
        int index = 1;
        foreach (var jsonData in data)
        {
            jsonString += JsonUtility.ToJson(jsonData, true);
            if (index < data.Count)
            {
                jsonString += ",\n";
            }
            index++;
        }
        jsonString += "]}";

        fileWriter.Write(jsonString);
        fileWriter.Close();
    }
}
