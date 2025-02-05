using UnityEngine;
using UnityEngine.Events;

public class MovementLimiter : MonoBehaviour
{
    [Header("Bounds Settings")]
    public Vector3 minBounds; // Minimum x, y, z limits
    public Vector3 maxBounds; // Maximum x, y, z limits

    [Header("Events")]
    public UnityEvent OnExitBounds; // Event triggered when out of bounds

    private void Update()
    {
        CheckBounds();
    }

    private void CheckBounds()
    {
        Vector3 clampedPosition = transform.position;
        bool outOfBounds = false;

        if (transform.position.x < minBounds.x || transform.position.x > maxBounds.x)
        {
            clampedPosition.x = Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x);
            outOfBounds = true;
        }
        if (transform.position.y < minBounds.y || transform.position.y > maxBounds.y)
        {
            clampedPosition.y = Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y);
            outOfBounds = true;
        }
        if (transform.position.z < minBounds.z || transform.position.z > maxBounds.z)
        {
            clampedPosition.z = Mathf.Clamp(transform.position.z, minBounds.z, maxBounds.z);
            outOfBounds = true;
        }

        // Apply clamping
        transform.position = clampedPosition;

        // Trigger event if the object was out of bounds
        if (outOfBounds)
        {
            OnExitBounds?.Invoke();
        }
    }
}