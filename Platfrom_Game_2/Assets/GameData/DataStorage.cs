using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class DataStorage
{
    private static string filePath = Application.persistentDataPath + "/gameSave.json";
    private static Dictionary<string, Dictionary<string, object>> gameData = new Dictionary<string, Dictionary<string, object>>();

    public static void SaveData(string key, Dictionary<string, object> data)
    {
        gameData[key] = data;
        SaveToFile();
    }

    public static Dictionary<string, object> LoadData(string key)
    {
        LoadFromFile();
        return gameData.ContainsKey(key) ? gameData[key] : null;
    }

    public static void SaveAllData(bool useFileStorage)
    {
        if (useFileStorage)
            SaveToFile();
    }

    public static void LoadAllData(bool useFileStorage)
    {
        if (useFileStorage)
            LoadFromFile();
    }

    private static void SaveToFile()
    {
        string json = JsonUtility.ToJson(new SerializationWrapper(gameData));
        File.WriteAllText(filePath, json);
    }

    private static void LoadFromFile()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<SerializationWrapper>(json).ToDictionary();
        }
    }

    [System.Serializable]
    private class SerializationWrapper
    {
        public List<string> keys;
        public List<string> values;

        public SerializationWrapper(Dictionary<string, Dictionary<string, object>> dictionary)
        {
            keys = new List<string>(dictionary.Keys);
            values = new List<string>();
            foreach (var value in dictionary.Values)
            {
                values.Add(JsonUtility.ToJson(value));
            }
        }

        public Dictionary<string, Dictionary<string, object>> ToDictionary()
        {
            var dict = new Dictionary<string, Dictionary<string, object>>();
            for (int i = 0; i < keys.Count; i++)
            {
                dict[keys[i]] = JsonUtility.FromJson<Dictionary<string, object>>(values[i]);
            }
            return dict;
        }
    }
}
