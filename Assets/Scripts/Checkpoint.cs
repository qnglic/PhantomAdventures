using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    [SerializeField] Killzone killzone;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            killzone.SetSpawnPosition(transform.position);
        }
    }

}
