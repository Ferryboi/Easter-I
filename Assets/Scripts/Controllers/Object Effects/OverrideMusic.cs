using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverrideMusic : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;

    private void Start()
    {
        source.Stop();
        source.clip = clip;
        source.Play();
    }
}
