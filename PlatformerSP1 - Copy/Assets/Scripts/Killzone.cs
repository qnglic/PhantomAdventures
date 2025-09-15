using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Killzone : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;
    // When an object connected to the script collides (triggers) with ex. a volume, this code will run
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if it's the player
        if (other.CompareTag("Player"))
        {
            other.transform.position = spawnPosition.position;
            other.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
    }
}
