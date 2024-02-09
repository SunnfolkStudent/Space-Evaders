using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            PlayerEvents.hitAsteroid?.Invoke();
        }
        
        if (other.CompareTag("Laser"))
        {
            PlayerEvents.hitLaserCharge?.Invoke();
        }
        
        if (other.CompareTag("Shield"))
        {
            PlayerEvents.hitShield?.Invoke();
        }
        
        GameEvents.destroyAsteroid?.Invoke(other.gameObject);
    }
}
