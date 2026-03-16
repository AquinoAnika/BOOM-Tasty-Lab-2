using UnityEngine;

public class TriggerEventScript : MonoBehaviour
{
    // This function is called when another collider enters the trigger collider attached to this object
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger has a specific tag (e.g., "Player")
        if (other.CompareTag("Player"))
        {
            Debug.Log("THATS A TANK!");
            // Add the action you want to happen here (e.g., start a cutscene, open a door, play a sound)
            // Example: OpenDoor(); or other methods
        }
    }

    // You can also use OnTriggerExit to trigger an event when an object leaves the area
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("didnt spot us...good");
        }
    }
}
