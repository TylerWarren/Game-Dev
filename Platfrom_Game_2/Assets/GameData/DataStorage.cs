using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataStorage", menuName = "Utilities/Data Storage Object")]
public class DataStorage : ScriptableObject
{
    public ScriptableObject data; // Primary data (e.g., score)
    public List<ScriptableObject> listData; // Additional data (e.g., door, checkpoint)

    private void SaveData<T>(T obj) where T : UnityEngine.Object
    {
        if (obj == null)
        {
            Debug.LogWarning("Cannot save null object");
            return;
        }

        string json = JsonUtility.ToJson(obj);
        if (string.IsNullOrEmpty(json))
        {
            Debug.LogWarning($"Failed to serialize {obj.name} to JSON");
            return;
        }

        PlayerPrefs.SetString(GetUniqueKey(obj), json);
        Debug.Log($"Saved {obj.name}: {json}");
    }

    private void LoadData<T>(T obj) where T : UnityEngine.Object
    {
        if (obj == null)
        {
            Debug.LogWarning("Cannot load into null object");
            return;
        }

        string key = GetUniqueKey(obj);
        if (PlayerPrefs.HasKey(key))
        {
            string json = PlayerPrefs.GetString(key);
            JsonUtility.FromJsonOverwrite(json, obj);
            Debug.Log($"Loaded {obj.name}: {json}");
        }
        else
        {
            Debug.Log($"No saved data found for {obj.name}");
        }
    }

    public void SaveAllData()
    {
        if (data != null)
        {
            SaveData(data);
        }
        else
        {
            Debug.LogWarning("Primary data is null; skipping save");
        }

        if (listData != null)
        {
            foreach (var obj in listData)
            {
                SaveData(obj);
            }
        }
        else
        {
            Debug.LogWarning("listData is null; skipping save");
        }

        PlayerPrefs.Save(); // Ensure data is written to disk
    }

    public void LoadAllData()
    {
        if (data != null)
        {
            LoadData(data);
        }
        else
        {
            Debug.LogWarning("Primary data is null; skipping load");
        }

        if (listData != null)
        {
            foreach (var obj in listData)
            {
                LoadData(obj);
            }
        }
        else
        {
            Debug.LogWarning("listData is null; skipping load");
        }
    }

    public void ClearAllData()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("All PlayerPrefs data cleared");
    }

    // Helper to generate unique keys for PlayerPrefs
    private string GetUniqueKey(UnityEngine.Object obj)
    {
        // Use a combination of object name and type to avoid conflicts
        return $"{obj.GetType().Name}_{obj.name}";
    }

    // Save/Load from GameObject (unchanged but included for completeness)
    public void SaveDataFromGameObject(GameObject obj)
    {
        var storage = obj.GetComponent<DataStorage>();
        if (storage == null) return;
        storage.SaveAllData();
    }

    public void LoadDataFromGameObject(GameObject obj)
    {
        var storage = obj.GetComponent<DataStorage>();
        if (storage == null) return;
        storage.LoadAllData();
    }
}