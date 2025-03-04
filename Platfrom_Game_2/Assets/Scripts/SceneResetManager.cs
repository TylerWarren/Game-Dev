using GameActions;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneResetManager : MonoBehaviour
{
    [SerializeField] private GameAction restartGameAction; // Assign in Inspector
    [SerializeField] private float defaultRespawnDelay = 5.0f; // Default delay

    private void OnEnable()
    {
        if (restartGameAction == null)
        {
            Debug.LogError("RestartGameAction is not assigned!", this);
            return;
        }

        restartGameAction.Raise += OnPlayerKilled;
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
            float delay = defaultRespawnDelay;

            // Check if the event parameter contains a delay (optional)
            if (obj is RespawnData respawnData)
            {
                delay = respawnData.RespawnDelay;
            }

            Debug.Log($"Player {player.name} was killed. Respawning in {delay} seconds...");
            StartCoroutine(RespawnAfterDelay(delay));
        }
        else
        {
            Debug.LogError("GameAction was triggered, but the object is not a GameObject!");
        }
    }

    private IEnumerator RespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ✅ Update delay dynamically
    public void SetRespawnDelay(float delay)
    {
        defaultRespawnDelay = delay;
        Debug.Log($"Respawn delay updated to {defaultRespawnDelay} seconds.");
    }
}

// ✅ Helper class to pass a dynamic delay
public class RespawnData
{
    public float RespawnDelay { get; private set; }

    public RespawnData(float delay)
    {
        RespawnDelay = delay;
    }
}