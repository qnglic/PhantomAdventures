using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class Gate : MonoBehaviour
{
    [SerializeField] private GameObject box;

    private Animator anim;
    private bool hasPlayedAnimation = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasPlayedAnimation)
        {
            box.SetActive(false);
            hasPlayedAnimation = true;
            anim.SetTrigger("Move");
        }
    }
}
