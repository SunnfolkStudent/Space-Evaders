using UnityEngine;
using UnityEngine.UI;

public class PlayerLaser : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Image[] laserChargeImages;
    
    private Vector2 _laserVelocity = new Vector2(0, 10);
    private int _maxLaserCharge = 3;
    private int _laserCharge;
    private float _laserCooldown = 1.5f;
    private float _laserCooldownTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        //Disable all laser charge images
        for (int i = 0; i < laserChargeImages.Length; i++)
        {
            DisableLaserChargeImage(i);
        }
        
        PlayerEvents.hitLaserCharge += AddLaserCharge;
        PlayerEvents.fireLaser += FireLaser;
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
            
            for (int i = 0; i < laserChargeImages.Length; i++)
            {
                EnableLaserChargeImage(i);
            }
        }
    }
    
    private void FireLaser()
    {
        if (_laserCharge > 0 && _laserCooldownTimer >= _laserCooldown)
        {
            _laserCharge--;
            _laserCooldownTimer = 0;
            DisableLaserChargeImage(_laserCharge);
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = _laserVelocity;
        }
    }
    
    private void DisableLaserChargeImage(int index)
    {
        laserChargeImages[index].enabled = false;
    }
    
    private void EnableLaserChargeImage(int index)
    {
        laserChargeImages[index].enabled = true;
    }
}
