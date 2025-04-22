using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataStorage", menuName = "Utilities/Data Storage Object")]
public class DataStorage : ScriptableObject
{
    public ScriptableObject data; // Can be null now
    public List<ScriptableObject> listData;

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

        if (listData != null)
        {
            foreach (var obj in listData)
            {
                SaveData(obj);
            }
        }

        PlayerPrefs.Save();
    }

    public void LoadAllData()
    {
        if (data != null)
        {
            LoadData(data);
        }

        if (listData != null)
        {
            foreach (var obj in listData)
            {
                LoadData(obj);
            }
        }
    }

    public void ClearAllData()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("All PlayerPrefs data cleared");
    }

    public void ClearSavedDataFor(ScriptableObject obj)
    {
        string key = GetUniqueKey(obj);
        if (PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.DeleteKey(key);
            Debug.Log($"Cleared saved data for {obj.name}");
        }
    }

    private string GetUniqueKey(UnityEngine.Object obj)
    {
        return $"{obj.GetType().Name}_{obj.name}";
    }
}
