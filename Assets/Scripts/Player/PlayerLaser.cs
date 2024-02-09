using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    private int _maxLaserCharge = 3;
    private int _laserCharge = 0;
    private float _laserCooldown = 1.5f;
    private float _laserCooldownTimer = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerEvents.hitLaserCharge += AddLaserCharge;
    }

    // Update is called once per frame
    void Update()
    {
        _laserCooldownTimer += Time.deltaTime;
    }
    
    private void AddLaserCharge()
    {
        if (_laserCharge < _maxLaserCharge)
        {
            _laserCharge = _maxLaserCharge;
        }
    }
    
    private void FireLaser()
    {
        if (_laserCharge > 0 && _laserCooldownTimer >= _laserCooldown)
        {
            _laserCharge--;
            _laserCooldownTimer = 0;
            // Fire laser
        }
    }
}
