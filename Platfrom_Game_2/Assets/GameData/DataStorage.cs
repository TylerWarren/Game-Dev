using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataStorage", menuName = "Utilities/Data Storage Object")]
public class DataStorage : ScriptableObject
{
    public ScriptableObject data;
    public List<ScriptableObject> listData;

    private void SaveData<T>(T obj) where T : UnityEngine.Object // Fix: Use UnityEngine.Object
    {
        if (obj == null) return;
        PlayerPrefs.SetString(obj.name, JsonUtility.ToJson(obj));
    }

    private void LoadData<T>(T obj) where T : UnityEngine.Object // Fix: Use UnityEngine.Object
    {
        if (obj == null) return;
        var jsonData = PlayerPrefs.GetString(obj.name);
        if (!string.IsNullOrEmpty(jsonData))
            JsonUtility.FromJsonOverwrite(jsonData, obj);
    }

    public void SaveAllData()
    {
        SaveData(data);
        foreach (var obj in listData)
        {
            SaveData(obj);
        }
    }
    
    public void LoadAllData()
    {
        LoadData(data);
        foreach (var obj in listData)
        {
            LoadData(obj);
        }
    }
    
    public void ClearAllData()
    {
        PlayerPrefs.DeleteAll();
    }
    
    // Save variables from a GameObject
    public void SaveDataFromGameObject(GameObject obj)
    {
        var data = obj.GetComponent<DataStorage>();
        if (data == null) return;
        data.SaveAllData();
    }
    
    // Get variables from a GameObject
    public void LoadDataFromGameObject(GameObject obj)
    {
        var data = obj.GetComponent<DataStorage>();
        if (data == null) return;
        data.LoadAllData();
    }
}