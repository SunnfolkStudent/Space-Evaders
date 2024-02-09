using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Spawnable Objects")]
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private GameObject laserChargePrefab;
    [SerializeField] private GameObject shieldChargePrefab;
    
    [Header("Spawn Rate and Velocity")]
    private float _timer;
    private float _spawnRate = 1.5f;
    private readonly float _spawnRateDecreasePerSecond = 0.99f;
    private float _asteroidVelocity = 2f;
    private readonly float _velocityIncreasePerSecond = 0.05f;
    private readonly float _maxAsteroidVelocity = 10f;
    private List<GameObject> _asteroids = new List<GameObject>();
    private float _lootVelocity = 1f;
    private List<GameObject> _loot = new List<GameObject>();
    
    [Header("Screen Size")]
    float _screenWidth = Screen.width;
    float _screenHeight = Screen.height;
    
    [Header("Despawn Position")]
    private Vector3 _despawnPosition;
    
    [Header("Score")]
    private float _score;
    private int _highScore;
    private bool _isGameOver;
    private TextMeshProUGUI _scoreText;
    private TextMeshProUGUI _highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        _despawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));

        PlayerEvents.playerDeath += OnDeath;
        GameEvents.destroyAsteroid += DestroyAsteroid;
        GameEvents.destroyLoot += DestroyLoot;

        _highScore = ReadWriteScore.ReadPersistentCopy();
        _highScoreText.text = "High Score: " + _highScore;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        UpdateDifficulty();
        
        UpdateAsteroids();
        
        CheckForSpawn();
        
        UpdateScore();
    }
    
    private void UpdateDifficulty()
    {
        if (_asteroidVelocity < _maxAsteroidVelocity)
        {
            _asteroidVelocity += _velocityIncreasePerSecond * Time.deltaTime;   
        }
        _spawnRate *= Mathf.Pow(_spawnRateDecreasePerSecond, Time.deltaTime);
    }

    private void UpdateAsteroids()
    {
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
    }
    
    private void CheckForSpawn()
    {
        if (_timer >= _spawnRate)
        {
            SpawnAtRandomLocation();
            _timer = 0;
        }
    }
    
    private void UpdateScore()
    {
        if (!_isGameOver)
        {
            _scoreText.text = "Score: " + (int)_score;
            _score += Time.deltaTime * _asteroidVelocity;   
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
        GameObject newObject = Instantiate(objectToSpawn, worldPosition, Quaternion.identity);

        switch (objectToSpawn)
        {
            case not null when objectToSpawn == shieldChargePrefab:
                newObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -_lootVelocity);
                _loot.Add(newObject);
                break;
            case not null when objectToSpawn == laserChargePrefab:
                newObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -_lootVelocity);
                _loot.Add(newObject);
                break;
            case not null when objectToSpawn == asteroidPrefab:
                newObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -_asteroidVelocity);
                _asteroids.Add(newObject);
                break;
        }
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
    
    private void DestroyLoot(GameObject loot)
    {
        _loot.Remove(loot);
        Destroy(loot);
    }
}
