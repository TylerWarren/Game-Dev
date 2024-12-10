using UnityEngine;

public class GameSpawner : MonoBehaviour
{
    public GameObject playerPrefab; // Reference to the player prefab
    public Transform spawnPoint;   // Reference to the spawn point

    void Start()
    {
        if (playerPrefab != null && spawnPoint != null)
        {
            Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        else
        {
            Debug.LogError("PlayerPrefab or SpawnPoint is not assigned!");
        }
    }
}