using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public enum PathType { Linear, Looping }
    
    [Header("Platform Settings")]
    public float speed = 2f;
    public PathType pathType = PathType.Linear;
    public Transform[] waypoints; // Assign waypoints in the Inspector
    
    private int currentWaypointIndex = 0;
    private bool isMoving = true;
    private int direction = 1; // 1 for forward, -1 for reverse

    private void FixedUpdate()
    {
        if (isMoving && waypoints.Length > 1)
        {
            MovePlatform();
        }
    }

    private void MovePlatform()
    {
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            UpdateWaypointIndex();
        }
    }

    private void UpdateWaypointIndex()
    {
        currentWaypointIndex += direction;

        if (pathType == PathType.Looping)
        {
            if (currentWaypointIndex >= waypoints.Length)
                currentWaypointIndex = 0;
            else if (currentWaypointIndex < 0)
                currentWaypointIndex = waypoints.Length - 1;
        }
        else // Linear Path
        {
            if (currentWaypointIndex >= waypoints.Length || currentWaypointIndex < 0)
            {
                ReverseDirection();
                currentWaypointIndex += direction; // Correct after reversal
            }
        }
    }

    public void StartMovement()
    {
        isMoving = true;
    }

    public void PauseMovement()
    {
        isMoving = false;
    }

    public void ReverseDirection()
    {
        direction *= -1;
    }
}