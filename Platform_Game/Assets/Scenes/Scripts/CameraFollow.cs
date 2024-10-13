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
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}