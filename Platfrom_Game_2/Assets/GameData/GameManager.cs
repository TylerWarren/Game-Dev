using UnityEngine;

public class GameManager : MonoBehaviour
{
    public DataStorage dataStorage; // Assign in Inspector

    void Start()
    {
        // Load data when the game starts
        dataStorage.LoadAllData(useFileStorage: true); // Use JSON files for larger data
    }

    void OnApplicationQuit()
    {
        // Save data when the game exits
        dataStorage.SaveAllData(useFileStorage: true);
    }

    // Example: Save data manually (e.g., after collecting an item)
    public void SaveGame()
    {
        dataStorage.SaveAllData(useFileStorage: true);
    }

    // Example: Load data manually (e.g., at a checkpoint)
    public void LoadGame()
    {
        dataStorage.LoadAllData(useFileStorage: true);
    }
}
