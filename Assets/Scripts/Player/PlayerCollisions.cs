using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            PlayerEvents.hitAsteroid?.Invoke();
            GameEvents.destroyAsteroid?.Invoke(other.gameObject);
        }
        
        if (other.CompareTag("LaserCharge"))
        {
            PlayerEvents.hitLaserCharge?.Invoke();
            GameEvents.destroyLoot?.Invoke(other.gameObject);
        }
        
        if (other.CompareTag("Shield"))
        {
            PlayerEvents.hitShield?.Invoke();
            GameEvents.destroyLoot?.Invoke(other.gameObject);
        }
    }
}
