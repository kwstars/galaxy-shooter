using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;

    private void Start()
    {
        // Reset position to origin
        transform.position = Vector3.zero;
    }

    private void Update()
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        // Get input from keyboard
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Create direction vector
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        // Move the player based on input, speed and delta time
        transform.Translate(direction * _speed * Time.deltaTime);
    }
}