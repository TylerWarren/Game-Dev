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
    [SerializeField] private CharacterRespawn player;

    private void Start()
    {
        if (!ValidateComponents("Start")) return;

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

        LoadGame();
    }

    public void SaveGame()
    {
        if (!ValidateComponents("Save")) return;

        if (checkpointData != null && player != null)
        {
            checkpointData.SetCheckpoint(player.lastCheckpoint);
        }

        dataStorage.SaveAllData();
        Debug.Log($"[Save] Score: {scoreData.value}, Door Open: {doorData?.isOpen}, Checkpoint: {checkpointData?.CheckpointPosition}");
    }

    public void LoadGame()
    {
        if (!ValidateComponents("Load")) return;

        dataStorage.LoadAllData();
        ResetDoorState();

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

    private void ResetPlayerPosition()
    {
        if (player != null && checkpointData != null)
        {
            Vector3 checkpoint = checkpointData.CheckpointPosition;
            if (checkpoint == Vector3.zero) checkpoint = player.initialPosition;
            player.SetCheckpoint(checkpoint);
            StartCoroutine(DelayedRespawn(checkpoint));
        }
    }

    private IEnumerator DelayedRespawn(Vector3 position)
    {
        yield return null;
        player.Respawn();
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

    private bool ValidateComponents(string context)
    {
        if (dataStorage == null)
        {
            Debug.LogError($"{context}: DataStorage is null.");
            return false;
        }
        if (scoreData == null)
        {
            Debug.LogError($"{context}: ScoreData is null.");
            return false;
        }
        if (player == null)
        {
            Debug.LogError($"{context}: Player (CharacterRespawn) is null.");
            return false;
        }
        return true;
    }
    

    public void AddScore(int points)
    {
        scoreData?.UpdateValue(points);
    }

    // Testing utility methods
    public void TestAddScore() => AddScore(10);

    public void TestOpenDoor()
    {
        if (door != null) door.OpenDoor();
        else Debug.LogWarning("Door not assigned");
    }

    public void TestCloseDoor()
    {
        if (door != null) door.CloseDoor();
        else Debug.LogWarning("Door not assigned");
    }
}
