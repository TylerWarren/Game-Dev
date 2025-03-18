using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DataStorage dataStorage;
    [SerializeField] private IntData scoreData; // Changed from PlayerData to IntData
    [SerializeField] private DoorData doorData;
    [SerializeField] private DoorController door;

    private void Start()
    {
        if (dataStorage == null)
        {
            Debug.LogError("DataStorage is not assigned in the Inspector! Please assign it.");
            return;
        }

        if (scoreData == null)
        {
            Debug.LogError("ScoreData (IntData) is not assigned in the Inspector! Please assign it.");
            return;
        }

        dataStorage.data = scoreData; // Set IntData as the primary data
        if (dataStorage.listData == null)
        {
            dataStorage.listData = new List<ScriptableObject>();
        }

        if (doorData != null && !dataStorage.listData.Contains(doorData))
        {
            dataStorage.listData.Add(doorData);
        }
        else if (doorData == null)
        {
            Debug.LogWarning("DoorData is not assigned in the Inspector. Door saving will be skipped.");
        }

        // Load saved data after OnEnable has reset the score to 0
        LoadGame();
    }

    public void SaveGame()
    {
        if (!ValidateComponents("Save")) return;

        Debug.Log($"Before Save - Score: {scoreData.value}, Door Open: {(doorData != null ? doorData.isOpen.ToString() : "N/A")}, Door Pos: {(doorData != null ? doorData.position.ToString() : "N/A")}");
        
        dataStorage.SaveAllData();

        Debug.Log($"Data Saved Successfully - Score: {scoreData.value}, Door Open: {(doorData != null ? doorData.isOpen.ToString() : "N/A")}, Door Pos: {(doorData != null ? doorData.position.ToString() : "N/A")}");
    }

    public void LoadGame()
    {
        if (!ValidateComponents("Load")) return;

        Debug.Log($"Before Load - Score: {scoreData.value}, Door Open: {(doorData != null ? doorData.isOpen.ToString() : "N/A")}, Door Pos: {(doorData != null ? doorData.position.ToString() : "N/A")}");
        
        dataStorage.LoadAllData();
        ResetDoorState();

        Debug.Log($"Data Loaded Successfully - Score: {scoreData.value}, Door Open: {(doorData != null ? doorData.isOpen.ToString() : "N/A")}, Door Pos: {(doorData != null ? doorData.position.ToString() : "N/A")}");
    }

    public void ResetGame()
    {
        if (!ValidateComponents("Reset")) return;

        Debug.Log($"Before Reset - Score: {scoreData.value}, Door Open: {(doorData != null ? doorData.isOpen.ToString() : "N/A")}, Door Pos: {(doorData != null ? doorData.position.ToString() : "N/A")}");
        
        scoreData.SetValue(0); // Reset score using IntData's method
        if (doorData != null) doorData.Reset();
        if (door != null) door.CloseDoor();

        Debug.Log($"Game Reset (In-Memory) - Score: {scoreData.value}, Door Open: {(doorData != null ? doorData.isOpen.ToString() : "N/A")}, Door Pos: {(doorData != null ? doorData.position.ToString() : "N/A")}");
    }

    private void ResetDoorState()
    {
        if (door != null)
        {
            door.CloseDoor();
            if (doorData != null)
            {
                doorData.isOpen = false;
                doorData.position = door.transform.position;
            }
            Debug.Log("Door state reset to closed");
        }
        else if (doorData != null)
        {
            doorData.isOpen = false;
            doorData.position = Vector3.zero;
            Debug.Log("DoorData reset to closed (no DoorController assigned)");
        }
    }

    private bool ValidateComponents(string operation)
    {
        if (dataStorage == null)
        {
            Debug.LogError($"Cannot {operation}: DataStorage is null");
            return false;
        }
        if (scoreData == null)
        {
            Debug.LogError($"Cannot {operation}: ScoreData (IntData) is null");
            return false;
        }
        return true;
    }

    public void AddScore(int points)
    {
        if (scoreData != null)
        {
            scoreData.UpdateValue(points); // Use IntData's UpdateValue method
            Debug.Log($"Score Updated - New Score: {scoreData.value}");
        }
        else
        {
            Debug.LogError("Cannot update score: ScoreData (IntData) is null");
        }
    }

    // Test methods for manual invocation
    public void TestAddScore()
    {
        AddScore(10);
    }

    public void TestOpenDoor()
    {
        if (door != null)
        {
            door.OpenDoor();
            Debug.Log("Test: Door opened");
        }
        else
        {
            Debug.LogWarning("Cannot open door: DoorController is not assigned");
        }
    }

    public void TestCloseDoor()
    {
        if (door != null)
        {
            door.CloseDoor();
            Debug.Log("Test: Door closed");
        }
        else
        {
            Debug.LogWarning("Cannot close door: DoorController is not assigned");
        }
    }
}