using UnityEngine;

public class LaserScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            GameEvents.destroyAsteroid?.Invoke(other.gameObject);
        }
    }
}
