using UnityEngine; 

public class Checkpoint : MonoBehaviour
{
    private GameManager gameManager; // Renamed to follow naming convention

    // Unity calls this method
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); // Cache GameManager
    }

    // Unity calls this method
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterSideScroller player = other.GetComponent<CharacterSideScroller>();
            if (player != null)
            {
                player.SetCheckpoint(transform.position);
                Debug.Log($"Checkpoint set at position: {transform.position}"); // Log the checkpoint position
                if (gameManager != null)
                {
                    gameManager.SaveGame(); // Save the game state when checkpoint is reached
                }
            }
        }
    }
} 