using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "DataStorage", menuName = "Utilities/Data Storage Object")]
public class DataStorage : ScriptableObject
{
    public ScriptableObject data;
    public List<ScriptableObject> listData;

    private string GetFilePath(string fileName) => Application.persistentDataPath + $"/{fileName}.json";

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

    public void SaveAllData(bool useFileStorage = false)
    {
        if (useFileStorage)
        {
            SaveDataToFile(data);
            foreach (var obj in listData) SaveDataToFile(obj);
        }
        else
        {
            SaveDataToPrefs(data);
            foreach (var obj in listData) SaveDataToPrefs(obj);
            PlayerPrefs.Save();
        }
        Debug.Log($"Saved data: {JsonUtility.ToJson(data)}"); // Verify saved data
    }

    public void LoadAllData(bool useFileStorage = false)
    {
        if (useFileStorage)
        {
            LoadDataFromFile(data);
            foreach (var obj in listData) LoadDataFromFile(obj);
        }
        else
        {
            LoadDataFromPrefs(data);
            foreach (var obj in listData) LoadDataFromPrefs(obj);
        }
    }

    // Removed ClearAllData() function
    // public void ClearAllData() { ... } // This is now gone

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