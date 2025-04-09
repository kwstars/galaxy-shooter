using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    [Header("Laser Settings")]
    [SerializeField] private float _speed = 8f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Move the laser upwards based on speed and delta time
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        // Destroy the laser if it goes off-screen
        if (transform.position.y > 8f)
        {
            Destroy(gameObject);
        }
    }
}
