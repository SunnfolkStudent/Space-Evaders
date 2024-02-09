using System;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    private int _lives = 3;
    private int _maxShieldCharge = 3;
    private int _shieldCharge = 0;

    private void Start()
    {
        PlayerEvents.hitAsteroid += TakeDamage;
        PlayerEvents.hitShield += AddShieldCharge;
    }

    private void TakeDamage()
    {
        if (_shieldCharge > 0)
        {
            _shieldCharge--;
            return;
        }
        
        _lives--;
        if (_lives <= 0)
        {
            PlayerEvents.playerDeath?.Invoke();
        }
    }
    
    private void AddShieldCharge()
    {
        if (_shieldCharge < _maxShieldCharge)
        {
            _shieldCharge++;
        }
    }
}
