using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoroutineBehaviour : MonoBehaviour
{
    // UnityEvents for various stages of the coroutine
    public UnityEvent startGameEvent;
    public UnityEvent endGameEvent;
    public UnityEvent killEvent; // Event triggered when the object is killed

    // List of actions to be executed during each cycle of the coroutine
    private List<UnityAction> gameActions = new List<UnityAction>();

    // Reference to the WaitForFixedUpdate object
    private readonly WaitForFixedUpdate _waitForFixedUpdateObj = new WaitForFixedUpdate();

    // Control flags for the coroutine
    private bool isRunning = false;
    private bool isKilled = false;

    // Coroutine
    private IEnumerator RepeatFixedUpdate()
    {
        // Invoke the start event when the coroutine begins
        startGameEvent?.Invoke();

        while (isRunning && !isKilled)
        {
            yield return _waitForFixedUpdateObj;

            // Execute all actions in the list
            foreach (var action in gameActions)
            {
                action?.Invoke();
            }
        }

        // If killed, trigger the kill event; otherwise, trigger the normal end event
        if (isKilled)
        {
            killEvent?.Invoke();
        }
        else
        {
            endGameEvent?.Invoke();
        }

        // Reset kill state
        isKilled = false;
    }

    // Method to start the Coroutine
    public void StartRepeatedUpdate()
    {
        if (isRunning) return;
        isRunning = true;
        StartCoroutine(nameof(RepeatFixedUpdate));
    }

    // Method to stop the Coroutine
    public void StopRepeatedUpdate()
    {
        if (!isRunning) return;
        isRunning = false;
    }

    // Method to toggle the Coroutine
    public void ToggleRepeatedUpdate()
    {
        if (isRunning)
        {
            StopRepeatedUpdate();
        }
        else
        {
            StartRepeatedUpdate();
        }
    }

    // Method to kill the Coroutine and trigger the kill event
    public void Kill()
    {
        if (!isRunning) return;
        isKilled = true;
        isRunning = false; // Stops the coroutine loop
    }

    // Method to add a new game action
    public void AddGameAction(UnityAction action)
    {
        if (action != null && !gameActions.Contains(action))
        {
            gameActions.Add(action);
        }
    }

    // Method to remove a game action
    public void RemoveGameAction(UnityAction action)
    {
        if (action != null && gameActions.Contains(action))
        {
            gameActions.Remove(action);
        }
    }

    // Method to clear all game actions
    public void ClearGameActions()
    {
        gameActions.Clear();
    }
}