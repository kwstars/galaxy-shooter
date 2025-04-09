using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f; // Speed of the player

    // Start is called before the first frame update
    private void Start()
    {
        // Reset position to origin
        transform.position = Vector3.zero;
    }

    // Update is called once per frame
    private void Update()
    {
        CalculateMovement();
    }

    // Handle player movement calculations
    private void CalculateMovement()
    {
        // Move the player based on speed and delta time
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }
}