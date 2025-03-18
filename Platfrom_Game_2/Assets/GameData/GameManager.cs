using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DataStorage dataStorage; // Assign in Inspector
    [SerializeField] private PlayerData playerData;   // Player data with score
    [SerializeField] private DoorData doorData;       // Door data
    [SerializeField] private DoorController door;     // Reference to door controller

    private void Start()
    {
        // Initialize storage
        if (dataStorage != null)
        {
            dataStorage.data = playerData;
            if (dataStorage.listData == null)
                dataStorage.listData = new List<ScriptableObject>();

            // Add door data to storage
            if (doorData != null && !dataStorage.listData.Contains(doorData))
            {
                dataStorage.listData.Add(doorData);
            }

            // Load saved data at start and verify
            LoadGame();
        }
        else
        {
            Debug.LogError("DataStorage is not assigned in the Inspector!");
        }
    }

    private void SaveGame()
    {
        if (dataStorage == null || playerData == null) 
        {
            Debug.LogError("Cannot save: DataStorage or PlayerData is null");
            return;
        }

        // Log current values before saving
        Debug.Log($"Before Save - Score: {playerData.score}, Door Open: {(doorData != null ? doorData.isOpen.ToString() : "N/A")}, Door Pos: {(doorData != null ? doorData.position.ToString() : "N/A")}");

        // Update playerData with current values
        playerData.score = 100; // Replace with your actual score updating logic
        
        // Save all data
        dataStorage.SaveAllData();

        // Log confirmation after saving
        Debug.Log($"Data Saved Successfully - Score: {playerData.score}, Door Open: {(doorData != null ? doorData.isOpen.ToString() : "N/A")}, Door Pos: {(doorData != null ? doorData.position.ToString() : "N/A")}");
    }

    private void LoadGame()
    {
        if (dataStorage == null || playerData == null) 
        {
            Debug.LogError("Cannot load: DataStorage or PlayerData is null");
            return;
        }

        // Log values before loading
        Debug.Log($"Before Load - Score: {playerData.score}, Door Open: {(doorData != null ? doorData.isOpen.ToString() : "N/A")}, Door Pos: {(doorData != null ? doorData.position.ToString() : "N/A")}");

        // Load all saved data
        dataStorage.LoadAllData();
        
        // Apply loaded door state
        if (door != null && doorData != null)
        {
            door.transform.position = doorData.position;
            if (doorData.isOpen) door.OpenDoor();
            else door.CloseDoor();
        }

        // Log values after loading to confirm data was retrieved
        Debug.Log($"Data Loaded Successfully - Score: {playerData.score}, Door Open: {(doorData != null ? doorData.isOpen.ToString() : "N/A")}, Door Pos: {(doorData != null ? doorData.position.ToString() : "N/A")}");
    }

    private void ResetGame()
    {
        if (dataStorage == null || playerData == null) 
        {
            Debug.LogError("Cannot reset: DataStorage or PlayerData is null");
            return;
        }

        // Log values before reset
        Debug.Log($"Before Reset - Score: {playerData.score}, Door Open: {(doorData != null ? doorData.isOpen.ToString() : "N/A")}, Door Pos: {(doorData != null ? doorData.position.ToString() : "N/A")}");

        // Clear all saved data
        dataStorage.ClearAllData();
        
        // Reset data objects to default values
        playerData.Reset();
        if (doorData != null) doorData.Reset();
        
        // Reset door position
        if (door != null)
        {
            door.CloseDoor();
        }
        
        Debug.Log($"Game Reset - Score: {playerData.score}, Door Open: {(doorData != null ? doorData.isOpen.ToString() : "N/A")}, Door Pos: {(doorData != null ? doorData.position.ToString() : "N/A")}");
    }
    
    // Method to modify score
    public void AddScore(int points)
    {
        if (playerData != null)
        {
            playerData.score += points;
            Debug.Log($"Score Updated - New Score: {playerData.score}");
        }
    }
}