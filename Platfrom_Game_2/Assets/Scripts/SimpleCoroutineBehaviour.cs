using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SimpleCoroutineBehaviour : MonoBehaviour
{
    public float seconds = 1;
    private WaitForSeconds _waitForSeconds;
    public UnityEvent @event;

    private Coroutine _coroutine;
    public string targetTag = "Player"; // Set this to the tag of the object that should trigger the event

    private void Awake()
    {
        _waitForSeconds = new WaitForSeconds(seconds);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag) && _coroutine == null)
        {
            _coroutine = StartCoroutine(EventLoop());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag) && _coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private IEnumerator EventLoop()
    {
        while (true)
        {
            yield return _waitForSeconds;
            @event.Invoke();
        }
    }
}