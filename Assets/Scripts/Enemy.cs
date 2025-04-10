using UnityEngine;

/// <summary>
/// Controls enemy behavior including movement and collision handling
/// </summary>
public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private float _speed = 4f;

    [Header("Boundaries")]
    [SerializeField] private float _topBoundary = 7f;
    [SerializeField] private float _bottomBoundary = -5f;
    [SerializeField] private float _horizontalRange = 8f;

    private void Start()
    {
        ResetPosition();
    }

    private void Update()
    {
        MoveDown();
        CheckBounds();
    }

    private void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    private void CheckBounds()
    {
        if (transform.position.y < _bottomBoundary)
        {
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        float randomX = Random.Range(-_horizontalRange, _horizontalRange);
        transform.position = new Vector3(randomX, _topBoundary, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HandlePlayerCollision(other);
        }
        else if (other.CompareTag("Laser"))
        {
            HandleLaserCollision(other);
        }
    }

    private void HandlePlayerCollision(Collider playerCollider)
    {
        playerCollider.GetComponent<Player>()?.Damage();
        Destroy(gameObject);
    }

    private void HandleLaserCollision(Collider laserCollider)
    {
        Destroy(laserCollider.gameObject);
        Destroy(gameObject);
    }
}