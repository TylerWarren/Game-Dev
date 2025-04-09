using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DataStorage dataStorage;
    [SerializeField] private IntData scoreData;
    [SerializeField] private DoorData doorData;
    [SerializeField] private DoorController door;
    [SerializeField] private CheckpointData checkpointData;
    [SerializeField] private CharacterSideScroller player;

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

        if (player == null)
        {
            Debug.LogError("Player (CharacterSideScroller) is not assigned in the Inspector! Please assign it.");
            return;
        }

        // Initialize dataStorage
        dataStorage.data = scoreData;
        if (dataStorage.listData == null)
        {
            dataStorage.listData = new List<ScriptableObject>();
        }

        if (doorData != null && !dataStorage.listData.Contains(doorData))
        {
            dataStorage.listData.Add(doorData);
        }
        if (checkpointData != null && !dataStorage.listData.Contains(checkpointData))
        {
            dataStorage.listData.Add(checkpointData);
        }

        // Load data immediately and apply it
        LoadGame();
    } 
 

   private System.Collections.IEnumerator DelayedLoadGame()
   {
       yield return new WaitForEndOfFrame();
       LoadGame();
   } 

   public void SaveGame()
    {
        if (!ValidateComponents("Save")) return;

        if (checkpointData != null && player != null)
        {
            checkpointData.SetCheckpoint(player.lastCheckpoint);
        }

        Debug.Log($"Before Save - Score: {scoreData.value}, Door Open: {(doorData != null ? doorData.isOpen.ToString() : "N/A")}, Checkpoint: {(checkpointData != null ? checkpointData.CheckpointPosition.ToString() : "N/A")}");
        
        dataStorage.SaveAllData();

        Debug.Log($"Data Saved Successfully - Score: {scoreData.value}, Door Open: {(doorData != null ? doorData.isOpen.ToString() : "N/A")}, Checkpoint: {(checkpointData != null ? checkpointData.CheckpointPosition.ToString() : "N/A")}");
    }

    public void LoadGame()
    {
        if (!ValidateComponents("Load")) return;

        Debug.Log($"Before Load - Score: {scoreData.value}, Door Open: {(doorData != null ? doorData.isOpen.ToString() : "N/A")}, Checkpoint: {(checkpointData != null ? checkpointData.CheckpointPosition.ToString() : "N/A")}");

        dataStorage.LoadAllData();
        ResetDoorState();

        // Set player position, fallback to initial if no checkpoint saved
        if (player != null)
        {
            if (checkpointData != null && checkpointData.CheckpointPosition != Vector3.zero)
            {
                player.lastCheckpoint = checkpointData.CheckpointPosition;
                ResetPlayerPosition();
            }
            else
            {
                player.lastCheckpoint = player.initialPosition;
                player.Respawn();
                Debug.Log("No valid checkpoint data; respawning at initial position");
            }
        }

        Debug.Log($"Data Loaded Successfully - Score: {scoreData.value}, Door Open: {(doorData != null ? doorData.isOpen.ToString() : "N/A")}, Checkpoint: {(checkpointData != null ? checkpointData.CheckpointPosition.ToString() : "N/A")}");
    } 
    
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void ResetGame()
    {
        if (!ValidateComponents("Reset")) return;
        
        scoreData.SetValue(0);
        dataStorage.SaveAllData();
        if (doorData != null) doorData.Reset();
        if (door != null) door.CloseDoor();

        if (checkpointData != null && player != null)
        {
            ResetPlayerPosition();
        }
        else
        {
            Debug.LogWarning("CheckpointData or Player is null; using initial position");
            if (player != null) player.Respawn();
        }
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
        }
        else if (doorData != null)
        {
            doorData.isOpen = false;
            doorData.position = Vector3.zero;
        }
    }

    private void ResetPlayerPosition()
    {
        if (player != null && checkpointData != null)
        {
            Vector3 loadedCheckpoint = checkpointData.CheckpointPosition;

            if (loadedCheckpoint != Vector3.zero)
            {
                player.SetCheckpoint(loadedCheckpoint);
                StartCoroutine(DelayedRespawn(loadedCheckpoint)); // ✅ Delayed
            }
            else
            {
                Debug.LogWarning("CheckpointData loaded zero vector. Using initial position.");
                player.SetCheckpoint(player.initialPosition);
                StartCoroutine(DelayedRespawn(player.initialPosition));
            }
        }
        else
        {
            Debug.LogWarning("Cannot reset player position: Player or CheckpointData is null");
        }
    }

    private IEnumerator DelayedRespawn(Vector3 position)
    {
        yield return null; // Wait one frame
        player.Respawn();
        
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
            scoreData.UpdateValue(points);
            
        }
    }
    

    // Test methods
    public void TestAddScore() { AddScore(10); }
    public void TestOpenDoor()
    {
        if (door != null) { door.OpenDoor(); Debug.Log("Test: Door opened"); }
        else { Debug.LogWarning("Cannot open door: DoorController is not assigned"); }
    }
    public void TestCloseDoor()
    {
        if (door != null) { door.CloseDoor(); Debug.Log("Test: Door closed"); }
        else { Debug.LogWarning("Cannot close door: DoorController is not assigned"); }
    }
}