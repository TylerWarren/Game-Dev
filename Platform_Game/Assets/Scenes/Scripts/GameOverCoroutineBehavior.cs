using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameOverCoroutineBehaviour : MonoBehaviour
{
    public float seconds = 1; // Delay before triggering the game over animation
    private WaitForSeconds _waitForSeconds;
    public UnityEvent gameOverEvent;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(seconds);
    }

    public void TriggerGameOver()
    {
        StartCoroutine(GameOverCoroutine());
    }

    private IEnumerator GameOverCoroutine()
    {
        // Wait until the player object is destroyed (or not active)
        while (GameObject.FindWithTag("Player") != null)
        {
            yield return null; // Wait for the next frame and check again
        }

        // Once the player is destroyed (or no longer found), trigger the game over event
        yield return _waitForSeconds; // Wait for the delay
        gameOverEvent.Invoke(); // Trigger the game over event (animation, etc.)
    }
}