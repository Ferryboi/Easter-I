using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinEffect : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 50f;
    private float startingDegree;

    private void Awake()
    {
        startingDegree = Random.Range(0, 360);
        transform.Rotate(Vector3.forward * startingDegree);
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward * spinSpeed * Time.deltaTime);
    }
}
