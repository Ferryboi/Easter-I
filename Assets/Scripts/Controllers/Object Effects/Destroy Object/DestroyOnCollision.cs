using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    [SerializeField] private GameObject parent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (parent) Destroy(parent);
        else Destroy(gameObject);
    }
}
