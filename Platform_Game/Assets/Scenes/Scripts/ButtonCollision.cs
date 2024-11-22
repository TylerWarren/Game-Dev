using UnityEngine;

public class ButtonCollision : MonoBehaviour
{
    public DoorCoroutineBehaviour doorCoroutine; // Reference to the DoorCoroutineBehaviour script

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player is interacting
        {
            doorCoroutine.TriggerEvent(); // Start the coroutine to open the door
        }
    }
}