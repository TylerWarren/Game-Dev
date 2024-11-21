using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CoroutineBehaviour : MonoBehaviour
{
    // UnityEvents for various stages of the coroutine
    public UnityEvent repeatUntilFalseEvent;
    public UnityEvent startGameEvent;
    public UnityEvent endGameEvent;

    // Reference to the WaitForFixedUpdate object
    private readonly WaitForFixedUpdate _waitForFixedUpdateObj = new WaitForFixedUpdate();

    // Control flag for the coroutine
    private bool isRunning = false;

    // Coroutine
    private IEnumerator RepeatFixedUpdate()
    {
        // Invoke the start event when the coroutine begins
        startGameEvent?.Invoke();

        while (isRunning)
        {
            yield return _waitForFixedUpdateObj;
            repeatUntilFalseEvent?.Invoke();
        }

        // Invoke the end event when the coroutine ends
        endGameEvent?.Invoke();
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
}