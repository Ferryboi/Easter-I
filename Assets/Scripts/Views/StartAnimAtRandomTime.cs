using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimAtRandomTime : MonoBehaviour
{
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        float animDuration = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;

        float time = Random.Range(0, animDuration);

        animator.Play(0, 0, time);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
