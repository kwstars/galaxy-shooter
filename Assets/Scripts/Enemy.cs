using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Enemy Settings")]
    [SerializeField] private float _speed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial position of the enemy to a random x position at the top of the screen
        transform.position = new Vector3(Random.Range(-8f, 8f), 5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Move the enemy downwards
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        // Check if the enemy has gone off-screen
        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enemy hit the player!");
            Destroy(gameObject); // Destroy the enemy
        }

        if (other.CompareTag("Laser"))
        {
            Debug.Log("Enemy hit by laser!");
            Destroy(other.gameObject); // Destroy the laser
            Destroy(gameObject); // Destroy the enemy
        }
    }
}
