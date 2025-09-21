using UnityEngine;
using UnityEngine.Audio;

public class GravityFlip : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private AudioClip sound;
    private SpriteRenderer player;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
         player = playerRb.GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.PlayOneShot(sound, 2.0f);
            playerRb.gravityScale *= -1;
            player.flipY = true;
        }
    }

}
