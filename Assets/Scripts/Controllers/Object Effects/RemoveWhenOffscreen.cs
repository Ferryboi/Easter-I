using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveWhenOffscreen : MonoBehaviour
{
    [SerializeField] private GameObject parent;

    private void OnBecameInvisible()
    {
        Destroy(parent);
    }
}
