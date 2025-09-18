using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    private Killzone killzone;

    // Instead of manually putting in killzone to every checkpoint
    private void Start()
    {
        killzone = FindFirstObjectByType<Killzone>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            killzone.SetSpawnPosition(transform.position);
        }
    }

}
