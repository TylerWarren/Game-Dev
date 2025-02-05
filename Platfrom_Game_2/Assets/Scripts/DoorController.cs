using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Vector3 openPosition; // Target position when the door opens
    private Vector3 closedPosition; // Initial door position
    public float openSpeed = 2f; // Speed of door movement

    private bool isOpening = false;

    private void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + new Vector3(0, 5, 0); // Moves door 5 units UP
    }

    private void Update()
    {
        if (isOpening)
        {
            transform.position = Vector3.MoveTowards(transform.position, openPosition, openSpeed * Time.deltaTime);
        }
    }

    public void OpenDoor()
    {
        isOpening = true;
    }
}