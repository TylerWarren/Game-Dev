using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DataStorage dataStorage; // Assign in Inspector
    [SerializeField] private PlayerData playerData;   // Your data to save

    private void Start()
    {
        // Initialize storage
        dataStorage.data = playerData;
        if (dataStorage.listData == null)
            dataStorage.listData = new List<ScriptableObject>();

        // Load saved data at start
        dataStorage.LoadAllData();
    }

    private void SaveGame()
    {
        // Update playerData with current values
        playerData.score = 100;


        // Save all data
        dataStorage.SaveAllData();
    }

    private void LoadGame()
    {
        // Load all saved data
        dataStorage.LoadAllData();
        
        // Use the loaded values
        Debug.Log($"Score: {playerData.score}");

    }

    private void ResetGame()
    {
        // Clear all saved data
        dataStorage.ClearAllData();
    }

    // Example using GameObject methods
    public GameObject dataHolder;
    private void SaveFromObject()
    {
        dataStorage.SaveDataFromGameObject(dataHolder);
    }
}