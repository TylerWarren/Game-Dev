using UnityEngine;

public class PlayerPlatformAttach : MonoBehaviour
{
    private Transform originalParent;
    private Vector3 lastPlatformPosition;
    private bool isOnPlatform = false;
    private Transform platformTransform;

    void Start()
    {
        originalParent = transform.parent; // Store the original parent of the player
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MovingPlatform"))
        {
            platformTransform = other.transform;
            lastPlatformPosition = platformTransform.position;
            isOnPlatform = true; // Flag to indicate the player is on the platform
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MovingPlatform"))
        {
            isOnPlatform = false; // Flag to indicate the player is no longer on the platform
            platformTransform = null;
        }
    }

    void FixedUpdate()
    {
        if (isOnPlatform && platformTransform != null)
        {
            Vector3 platformMovement = platformTransform.position - lastPlatformPosition;
            transform.position += platformMovement;
            lastPlatformPosition = platformTransform.position;
        }
    }
}