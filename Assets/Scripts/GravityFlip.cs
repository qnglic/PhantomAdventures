using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

public class GravityFlip : MonoBehaviour
{
    [SerializeField] private AudioClip sound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();

        if (other.CompareTag("Player"))
        {
            player.GravityFlip();
            audioSource.PlayOneShot(sound, 2.0f);
        }
    }
}
