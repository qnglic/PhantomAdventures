using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    private Killzone killzone;
    private PlayerMovement player;

    // Instead of manually putting in killzone to every checkpoint
    private void Start()
    {
        killzone = FindFirstObjectByType<Killzone>();
        player = FindFirstObjectByType<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            killzone.SetSpawnPosition(transform.position, player.isFlipped);
        }
    }

}
