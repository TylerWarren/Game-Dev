using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DoorCoroutineBehaviour : MonoBehaviour
{
    public float seconds = 5f; // Delay before triggering the door
    private WaitForSeconds _waitForSeconds;
    public UnityEvent @event; // Event to trigger after delay

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(seconds);
    }

    public void TriggerEvent()
    {
        StartCoroutine(StartCoroutineWithDelay());
    }

    private IEnumerator StartCoroutineWithDelay()
    {
        yield return _waitForSeconds; // Wait for the delay
        @event.Invoke(); // Trigger actions after delay
    }
}