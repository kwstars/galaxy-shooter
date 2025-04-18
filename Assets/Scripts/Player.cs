using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls player movement and screen boundaries
/// </summary>
public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _speedMultiplier = 2f;

    [Header("Boundary Settings")]
    [SerializeField] private float _topBoundary = 0f;
    [SerializeField] private float _bottomBoundary = -3.8f;
    [SerializeField] private float _horizontalBoundary = 11.3f;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private float _fireRate = 0.5f;
    private float _nextFireTime = 0f;
    [SerializeField] private int _lives = 3;

    private SpawnManager _spawnManager;

    [SerializeField] private bool _isTripleShotActive = false;
    [SerializeField] private bool _isSpeedBoostActive = false;
    [SerializeField] private bool _isShieldActive = false;
    [SerializeField] private GameObject _shieldVisualizer;

    private void Start()
    {
        // Reset position to origin
        transform.position = Vector3.zero;
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("SpawnManager is NULL");
        }
    }

    private void Update()
    {
        HandleMovement();
        RestrictPosition();
        FireLaser();
    }

    /// <summary>
    /// Handles laser firing
    /// </summary>
    private void FireLaser()
    {
        // Check for space key press to fire laser
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFireTime)
        {
            _nextFireTime = Time.time + _fireRate;

            if (_isTripleShotActive)
            {
                Instantiate(_tripleShotPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
            }
        }
    }

    private void HandleMovement()
    {
        // Get input from keyboard
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Create direction vector
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        // Move the player based on input, speed and delta time
        transform.Translate(direction * _speed * Time.deltaTime);
    }

    /// <summary>
    /// Restricts player position using clamping and wrapping
    /// </summary>
    private void RestrictPosition()
    {
        Vector3 position = transform.position;

        // Clamp vertical position using Mathf.Clamp
        position.y = Mathf.Clamp(position.y, _bottomBoundary, _topBoundary);

        // Handle horizontal wrapping
        if (Mathf.Abs(position.x) > _horizontalBoundary)
        {
            position.x = -Mathf.Sign(position.x) * _horizontalBoundary;
        }

        // Apply modified position
        transform.position = position;
    }

    /// <summary>
    /// Handles player damage
    /// </summary>
    public void Damage()
    {
        if (_isShieldActive)
        {
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }

        _lives--;
        if (_lives <= 0)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    private IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _isTripleShotActive = false;
    }

    public void SpeedBoostActive()
    {
        _isSpeedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    private IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _isSpeedBoostActive = false;
        _speed /= _speedMultiplier;
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
    }
}