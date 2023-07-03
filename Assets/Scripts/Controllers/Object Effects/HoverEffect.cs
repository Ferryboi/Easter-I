using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverEffect : MonoBehaviour
{
    private Vector3 startPos;
    [SerializeField] private float frequency = 0.25f;
    [SerializeField] private float altitude = 0.1f;
    private float startingDegree;

    private void Awake()
    {
        startingDegree = Random.Range(0, 360);
        startPos = transform.localPosition;
    }

    private void Update()
    {
        transform.localPosition = startPos + new Vector3(0, Mathf.Sin(startingDegree + (Time.fixedTime * Mathf.PI * frequency)) * altitude);
    }
}
