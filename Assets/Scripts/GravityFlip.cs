using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

public class GravityFlip : MonoBehaviour
{
    [SerializeField] private AudioClip sound;
    private AudioSource audioSource;
    private GameObject obj;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        obj = GetComponent<GameObject>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();

        if (other.CompareTag("Player"))
        {
            player.GravityFlip();
            audioSource.PlayOneShot(sound, 2.0f);
        }

        // to avoid bug when player jumps on top of object

        obj.SetActive(false);
        Invoke("Reactivate", 1f);
    }

    private void Reactivate()
    {
        obj.SetActive(true);
    }

}
