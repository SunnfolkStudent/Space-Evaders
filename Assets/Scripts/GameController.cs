using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private GameObject laserChargePrefab;
    [SerializeField] private GameObject shieldChargePrefab;
    
    
    private float _timer;
    private float _spawnRate = 1.5f;
    private readonly float _spawnRateDecreasePerSecond = 0.01f;
    private float _asteroidVelocity = 2f;
    private readonly float _velocityIncreasePerSecond = 0.1f;
    private List<GameObject> _asteroids = new List<GameObject>();

    float _screenWidth = Screen.width;
    float _screenHeight = Screen.height;
    private Vector3 _despawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        _despawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));

        PlayerEvents.playerDeath += OnDeath;
        GameEvents.destroyElement += DestroyAsteroid;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        _asteroidVelocity += _velocityIncreasePerSecond * Time.deltaTime;
        _spawnRate -= _spawnRateDecreasePerSecond * Time.deltaTime;

        List<GameObject> asteroidsToRemove = new List<GameObject>();

        foreach (var asteroid in _asteroids)
        {
            if (asteroid.transform.position.y < _despawnPosition.y - 1f)
            {
                asteroidsToRemove.Add(asteroid);
            }

            asteroid.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -_asteroidVelocity);
        }

        foreach (var asteroidToRemove in asteroidsToRemove)
        {
            _asteroids.Remove(asteroidToRemove);
            Destroy(asteroidToRemove);
        }

        if (_timer >= _spawnRate)
        {
            SpawnAtRandomLocation();
            _timer = 0;
        }
    }

    private void SpawnAtRandomLocation()
    {
        // Convert screen coordinates to world coordinates
        Vector3 spawnPosition = new Vector3(Random.Range(0f, _screenWidth), _screenHeight, 0f);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(spawnPosition);
        worldPosition.z = 0f; // Set the z-coordinate to 0 for 2D games

        GameObject objectToSpawn = DetermineSpawnObject();
        // Instantiate the object at the random location
        GameObject newAsteroid = Instantiate(objectToSpawn, worldPosition, Quaternion.identity);
        newAsteroid.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -_asteroidVelocity);
        _asteroids.Add(newAsteroid);
    }
    
    private GameObject DetermineSpawnObject()
    {
        int random = Random.Range(0, 100);
        if (random < 5)
        {
            return shieldChargePrefab;
        }
        else if (random < 10)
        {
            return laserChargePrefab;
        }
        else
        {
            return asteroidPrefab;
        }
    }

    private void OnDeath()
    {
        Time.timeScale = 0;
    }

    private void DestroyAsteroid(GameObject asteroid)
    {
        _asteroids.Remove(asteroid);
        Destroy(asteroid);
    }
}
