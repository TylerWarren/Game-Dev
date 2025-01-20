using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The object that the camera will follow
    public float smoothSpeed = 0.125f; // How smooth the camera follows the target
    public Vector3 offset; // Offset position from the target

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the desired position with the target's x and z, and the camera's current y
            Vector3 desiredPosition = new Vector3(
                target.position.x + offset.x,
                transform.position.y, // Keep the camera's current y position
                target.position.z + offset.z
            );

            // Smoothly interpolate towards the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}