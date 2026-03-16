using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Drag Player here
    public float smoothSpeed = 0.125f;
    public Vector3 offset; // Set camera position relative to player

    void LateUpdate()
    {
        // Smoothly interpolate between current position and target position
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Optional: Always look at the player
        transform.LookAt(target);
    }
}