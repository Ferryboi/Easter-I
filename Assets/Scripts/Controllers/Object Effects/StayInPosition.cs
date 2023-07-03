using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayInPosition : MonoBehaviour
{
    private Vector3 standardUp;
    private Vector3 offset;

    private void Awake()
    {
        standardUp = transform.up;
        offset = transform.localPosition;
    }

    private void Update()
    {
        transform.position = transform.parent.position + offset;
        transform.up = standardUp;
    }
}
