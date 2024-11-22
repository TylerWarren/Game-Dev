using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public GameOverCoroutineBehaviour gameOverCoroutine; // Reference to the GameOverCoroutineBehaviour

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // If colliding with an object tagged as "Enemy" (lava pit)
        {
            gameOverCoroutine.TriggerGameOver(); // Trigger the game over sequence
        }
    }
}