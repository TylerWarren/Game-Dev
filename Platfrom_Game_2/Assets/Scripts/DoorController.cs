using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private DoorData doorData; // Assign in Inspector
    
    public Vector3 openPosition;    // Target position when door opens
    private Vector3 closedPosition; // Initial door position
    public float openSpeed = 2f;    // Speed of door movement
    
    private bool isOpening = false;

    private void Start()
    {
        // Set initial positions
        closedPosition = transform.position;
        openPosition = closedPosition + new Vector3(0, 5, 0);

        // Load saved state
        if (doorData != null)
        {
            transform.position = doorData.position;
            isOpening = doorData.isOpen;
        }
    }

    private void Update()
    {
        if (isOpening)
        {
            transform.position = Vector3.MoveTowards(transform.position, openPosition, openSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, closedPosition, openSpeed * Time.deltaTime);
        }

        // Update saved data continuously
        if (doorData != null)
        {
            doorData.position = transform.position;
            doorData.isOpen = isOpening;
        }
    }

    public void OpenDoor()
    {
        isOpening = true;
    }

    public void CloseDoor()
    {
        isOpening = false;
    }

    public void ResetDoor()
    {
        throw new System.NotImplementedException();
    }
}