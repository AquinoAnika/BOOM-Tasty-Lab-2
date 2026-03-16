using UnityEngine;

public class UnstuckFeature : MonoBehaviour
{
    private Vector3 lastGroundPosition;
    public KeyCode unstuckKey = KeyCode.U;
    public float minMoveDistanceForSave = 1f; // To prevent abuse

    void Update()
    {
        // Update last ground position when the player moves significantly
        if (Vector3.Distance(transform.position, lastGroundPosition) > minMoveDistanceForSave)
        {
            // Add a check here if the player is grounded or on a valid surface
            // For a simple solution, just save the position
            lastGroundPosition = transform.position;
        }

        if (Input.GetKeyDown(unstuckKey))
        {
            // Teleport the player to the last valid position
            transform.position = lastGroundPosition + Vector3.up * 1f; // Add a small upward offset to prevent being stuck in the floor again
            // Reset any physics forces or velocity if using a Rigidbody
            if (TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}
