using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestroyAfterLifetime : MonoBehaviour
{
    [SerializeField] private float lifetime = 1.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

}
