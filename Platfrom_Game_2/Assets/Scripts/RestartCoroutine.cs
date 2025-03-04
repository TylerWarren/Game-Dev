using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class RestartCoroutine : MonoBehaviour
{
    [SerializeField] private float seconds = 1f;
    private WaitForSeconds _waitForSeconds;
    public UnityEvent @event;

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(seconds);
    }

    public void StartCoroutineDelay()
    {
        StartCoroutine(DelayedAction());
    }

    private IEnumerator DelayedAction()
    {
        yield return _waitForSeconds;
        @event.Invoke();
    }

    // Optional: Allow dynamic delay changes
    public void SetDelay(float newSeconds)
    {
        seconds = newSeconds;
        _waitForSeconds = new WaitForSeconds(seconds);
        Debug.Log($"Respawn delay set to {seconds} seconds.");
    }
}