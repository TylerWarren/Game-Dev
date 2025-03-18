using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "DataStorage", menuName = "Utilities/Data Storage Object")]
public class DataStorage : ScriptableObject
{
    public ScriptableObject data;
    public List<ScriptableObject> listData;

    private string GetFilePath(string fileName) => Application.persistentDataPath + $"/{fileName}.json";

    // Save to PlayerPrefs (for small data)
    private void SaveDataToPrefs<T>(T obj) where T : Object
    {
        if (obj == null) return;
        PlayerPrefs.SetString(obj.name, JsonUtility.ToJson(obj));
    }

    private void LoadDataFromPrefs<T>(T obj) where T : Object
    {
        if (obj == null) return;
        string jsonData = PlayerPrefs.GetString(obj.name, "");
        if (!string.IsNullOrEmpty(jsonData))
        {
            JsonUtility.FromJsonOverwrite(jsonData, obj);
        }
    }

    // Save to JSON File (for larger data)
    private void SaveDataToFile<T>(T obj) where T : Object
    {
        if (obj == null) return;
        string path = GetFilePath(obj.name);
        string json = JsonUtility.ToJson(obj, true);
        File.WriteAllText(path, json);
    }

    private void LoadDataFromFile<T>(T obj) where T : Object
    {
        if (obj == null) return;
        string path = GetFilePath(obj.name);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, obj);
        }
    }

    // Save all data
    public void SaveAllData(bool useFileStorage = false)
    {
        if (useFileStorage)
        {
            SaveDataToFile(data);
            foreach (var obj in listData)
            {
                SaveDataToFile(obj);
            }
        }
        else
        {
            SaveDataToPrefs(data);
            foreach (var obj in listData)
            {
                SaveDataToPrefs(obj);
            }
            PlayerPrefs.Save();
        }
    }

    // Load all data
    public void LoadAllData(bool useFileStorage = false)
    {
        if (useFileStorage)
        {
            LoadDataFromFile(data);
            foreach (var obj in listData)
            {
                LoadDataFromFile(obj);
            }
        }
        else
        {
            LoadDataFromPrefs(data);
            foreach (var obj in listData)
            {
                LoadDataFromPrefs(obj);
            }
        }
    }

    // Clear all saved data
    public void ClearAllData()
    {
        PlayerPrefs.DeleteAll();
        foreach (var obj in listData)
        {
            string path = GetFilePath(obj.name);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }

    // Save and load from GameObject
    public void SaveDataFromGameObject(GameObject obj, bool useFileStorage = false)
    {
        var data = obj.GetComponent<DataStorage>();
        if (data == null) return;
        data.SaveAllData(useFileStorage);
    }

    public void LoadDataFromGameObject(GameObject obj, bool useFileStorage = false)
    {
        var data = obj.GetComponent<DataStorage>();
        if (data == null) return;
        data.LoadAllData(useFileStorage);
    }
}