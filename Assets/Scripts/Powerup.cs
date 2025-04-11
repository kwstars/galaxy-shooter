using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [Header("Powerup Settings")]
    [SerializeField] private float _speed = 3f;
    [SerializeField] private int _powerUpID; // 0 = Triple Shot, 1 = Speed, 2 = Shields


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y < -4.5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                switch (_powerUpID)
                {
                    case 0:
                        player.TripleShotActive();
                        Debug.Log("Triple Shot Activated");
                        break;
                    case 1:
                        player.SpeedBoostActive();
                        Debug.Log("Speed Boost Activated");
                        break;
                    case 2:
                        // player.ShieldActive();
                        Debug.Log("Shield Activated");
                        break;
                    default:
                        Debug.LogError("Invalid PowerUp ID");
                        break;
                }
            }
            Destroy(gameObject);
        }
    }
}
