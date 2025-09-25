using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class Killzone : MonoBehaviour
{
    [SerializeField] public Transform spawnPosition;
    private PlayerMovement player;
    private bool spawnflip = false;

    // When an object connected to the script collides (triggers) with ex. a volume, this code will run
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if it's the player
        if (other.CompareTag("Player"))
        {        
            player = other.GetComponent<PlayerMovement>();

            other.transform.position = spawnPosition.position;
            other.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

            if (spawnflip != player.isFlipped)
            {
                player.GravityFlip();
            }
        }
    }

    public void SetSpawnPosition(Vector3 newSpawn, bool isFlipped)
    {
        spawnflip = isFlipped;
        spawnPosition.position = newSpawn;
    }
}
