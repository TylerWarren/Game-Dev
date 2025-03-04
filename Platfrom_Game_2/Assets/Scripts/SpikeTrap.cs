using UnityEngine;
using System.Collections;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private float extendHeight = 1.0f;     // How far the spikes extend upward
    [SerializeField] private float extendTime = 1.0f;       // Time to fully extend
    [SerializeField] private float holdTime = 0.5f;         // Time to hold at extended position
    [SerializeField] private float retractTime = 1.0f;      // Time to fully retract
    [SerializeField] private float moveSpeed = 2.0f;        // Speed of the extension/retraction motion

    private Vector3 retractedPosition;                      // Starting (down) position
    private Vector3 extendedPosition;                       // Target (up) position

    void Start()
    {
        // Store the initial position as the retracted state
        retractedPosition = transform.position;
        extendedPosition = retractedPosition + Vector3.up * extendHeight;

        // Start the spike trap cycle when the game begins
        StartCoroutine(SpikeTrapCycle());
    }

    // Coroutine to handle the trap's repeating cycle
    IEnumerator SpikeTrapCycle()
    {
        while (true) // Loop forever
        {
            // Extend the spikes
            yield return StartCoroutine(MoveToPosition(extendedPosition, extendTime));
            yield return new WaitForSeconds(holdTime); // Hold at extended position

            // Retract the spikes
            yield return StartCoroutine(MoveToPosition(retractedPosition, retractTime));
            yield return new WaitForSeconds(retractTime); // Stay retracted for a bit
        }
    }

    // Coroutine to smoothly move the trap between positions over a specific duration
    IEnumerator MoveToPosition(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration); // Normalized time (0 to 1)
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null; // Wait for the next frame
        }
        transform.position = targetPosition; // Snap to exact position
    }
}