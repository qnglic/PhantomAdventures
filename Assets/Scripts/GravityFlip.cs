using UnityEngine;

public class GravityFlip : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRb;
    private SpriteRenderer player;

    private void Start()
    {
         player = playerRb.GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerRb.gravityScale *= -1;
            player.flipY = true;
        }
    }

}
