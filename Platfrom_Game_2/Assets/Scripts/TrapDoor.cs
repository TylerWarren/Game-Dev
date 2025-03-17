using UnityEngine;
using System.Collections;
public class TrapDoor : MonoBehaviour
{
    [SerializeField] private float slideDistance = 2.0f;    // How far the door slides horizontally
    [SerializeField] private float openTime = 0.5f;         // Time to fully open
    [SerializeField] private float detectionRadius = 2.0f;  // Radius to detect the player
    [SerializeField] private bool slideRight = true;        // Toggle to slide right (true) or left (false)

    private Vector3 closedPosition;                         // Starting (closed) position
    private Vector3 openPosition;                           // Slid (open) position
    private bool isOpen = false;                            // Tracks if the door is already open
    private Coroutine trapCoroutine;                        // Reference to the running coroutine

    void Start()
    {
        // Store the initial position as the closed state
        closedPosition = transform.position;
        // Slide right or left based on the slideRight toggle
        Vector3 slideDirection = slideRight ? Vector3.right : Vector3.left;
        openPosition = closedPosition + slideDirection * slideDistance;
    }

    void Update()
    {
        // Only check for opening if the door isn't already open
        if (!isOpen)
        {
            // Check if the player is near using a distance check
            GameObject player = GameObject.FindGameObjectWithTag("Player"); // Assumes player has "Player" tag
            if (player != null)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
                if (distanceToPlayer <= detectionRadius)
                {
                    if (trapCoroutine != null) StopCoroutine(trapCoroutine); // Stop any existing coroutine
                    trapCoroutine = StartCoroutine(TrapDoorCycle());    // Open the door
                    isOpen = true;                                      // Mark as open to prevent re-triggering
                }
            }
        }
    }

    // Coroutine to handle opening the trap door
    IEnumerator TrapDoorCycle()
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = openPosition;
        float elapsedTime = 0f;

        while (elapsedTime < openTime)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / openTime); // Normalized time (0 to 1)
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null; // Wait for the next frame
        }
        transform.position = targetPosition; // Snap to exact position
    }

    // Optional: Visualize the detection radius in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}