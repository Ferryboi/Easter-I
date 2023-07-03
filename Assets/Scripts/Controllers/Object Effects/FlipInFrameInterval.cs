using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipInFrameInterval : MonoBehaviour
{
    [SerializeField] private Vector3 flip;
    [SerializeField] private int frames;

    private void Start()
    {
        StartCoroutine(Flip());
    }

    IEnumerator Flip()
    {
        while(true)
        {
            for (int i = 0; i < frames; i++)
            {
                yield return 0;
            }

            transform.Rotate(flip);
        }
    }
}
