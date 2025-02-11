using GameActions;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneResetManager : MonoBehaviour
{
    [SerializeField] private GameAction restartGameAction; // Assign in Inspector
    [SerializeField] private float respawnDelay = 5.0f; // ⏳ Customizable delay

    private void OnEnable()
    {
        if (restartGameAction == null)
        {
            Debug.LogError("RestartGameAction is not assigned!", this);
            return;
        }

        restartGameAction.Raise += OnPlayerKilled; // Subscribe to GameAction with object parameter
    }

    private void OnDisable()
    {
        if (restartGameAction != null)
        {
            restartGameAction.Raise -= OnPlayerKilled;
        }
    }

    private void OnPlayerKilled(object obj)
    {
        if (obj is GameObject player)
        {
            Debug.Log($"Player {player.name} was killed. Respawning in {respawnDelay} seconds...");
            StartCoroutine(RespawnAfterDelay());
        }
        else
        {
            Debug.LogError("GameAction was triggered, but the object is not a GameObject!");
        }
    }

    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ✅ Make the delay configurable via GameActionHandler
    public void SetRespawnDelay(float delay)
    {
        respawnDelay = delay;
        Debug.Log($"Respawn delay updated to {respawnDelay} seconds.");
    }
}