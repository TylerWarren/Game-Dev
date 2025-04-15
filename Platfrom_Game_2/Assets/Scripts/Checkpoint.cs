using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterRespawn player = other.GetComponent<CharacterRespawn>();
            if (player != null)
            {
                player.SetCheckpoint(transform.position);
                Debug.Log($"Checkpoint set at position: {transform.position}");

                if (gameManager != null)
                {
                    gameManager.SaveGame();
                }
            }
        }
    }
}