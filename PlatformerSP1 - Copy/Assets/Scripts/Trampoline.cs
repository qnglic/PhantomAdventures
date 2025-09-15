using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Trampoline : MonoBehaviour
{
    [SerializeField] private float jumpForce = 200f;
    [SerializeField] private AudioClip jumpTrampolineSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Animator>().SetTrigger("Jump");
            Rigidbody2D playerRigidbody = other.GetComponent<Rigidbody2D>();
            playerRigidbody.linearVelocity = new Vector2(playerRigidbody.linearVelocity.x, 0);
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(jumpTrampolineSound, 0.5f);
        }
    }
}
