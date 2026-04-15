using UnityEngine;
public class Collectible : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Playermovement>(out Playermovement player))
        {
            player.UnlockThrowable();
            Destroy(gameObject);
        }
    }
}
