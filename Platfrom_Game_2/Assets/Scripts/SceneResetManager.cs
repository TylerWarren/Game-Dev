using GameActions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneResetManager : MonoBehaviour
{
    [SerializeField] private GameAction restartGameAction; // Assign in Inspector

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
            Debug.Log($"Player {player.name} was killed. Restarting scene...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            Debug.LogError("GameAction was triggered, but the object is not a GameObject!");
        }
    }
}