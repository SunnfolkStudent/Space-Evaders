using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField] Image[] livesImages;
    [SerializeField] Image[] shieldImages;
    
    private int _lives = 3;
    private int _maxShieldCharge = 3;
    private int _shieldCharge = 0;

    private void Start()
    {
        //Disable all shield images
        for (int i = 0; i < shieldImages.Length; i++)
        {
            DisableShieldImage(i);
        }
        
        //enable all life images
        for (int i = 0; i < livesImages.Length; i++)
        {
            livesImages[i].enabled = true;
        }
        
        PlayerEvents.hitAsteroid += TakeDamage;
        PlayerEvents.hitShield += AddShieldCharge;
    }
    
    private void OnDestroy()
    {
        PlayerEvents.hitAsteroid -= TakeDamage;
        PlayerEvents.hitShield -= AddShieldCharge;
    }

    private void TakeDamage()
    {
        if (_shieldCharge > 0)
        {
            _shieldCharge--;
            DisableShieldImage(_shieldCharge);
            return;
        }
        
        _lives--;
        if (_lives <= 0)
        {
            PlayerEvents.playerDeath?.Invoke();
        }
        
        DisableLifeImage(_lives);
    }
    
    private void DisableLifeImage(int index)
    {
        livesImages[index].enabled = false;
    }
    
    private void AddShieldCharge()
    {
        if (_shieldCharge < _maxShieldCharge)
        {
            _shieldCharge++;
        }
        
        EnableShieldImage(_shieldCharge - 1);
    }
    
    private void EnableShieldImage(int index)
    {
        shieldImages[index].enabled = true;
    }
    
    private void DisableShieldImage(int index)
    {
        shieldImages[index].enabled = false;
    }
}
