using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Move the player to the right at a speed of 5 units per second
        transform.Translate(Vector3.right * Time.deltaTime * 5f);
        // transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * 5f);
        // transform.Translate(new Vector3(Time.deltaTime * 5f, 0, 0));
    }
}
