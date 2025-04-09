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

    [Header("Boundary Settings")]
    [SerializeField] private float _topBoundary = 0f;
    [SerializeField] private float _bottomBoundary = -3.8f;
    [SerializeField] private float _horizontalBoundary = 11.3f;
    [SerializeField] private GameObject _laserPrefab;

    private void Start()
    {
        // Reset position to origin
        transform.position = Vector3.zero;
    }

    private void Update()
    {
        HandleMovement();
        RestrictPosition();

        // Check for space key press to fire laser
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_laserPrefab, transform.position, Quaternion.identity);
        }
    }

    /// <summary>
    /// Processes player input and applies movement
    /// </summary>
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
}