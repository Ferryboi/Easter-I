using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string openAnim;
    [SerializeField] private string closeAnim;
    [SerializeField] private bool startOpened;

    public bool IsOpen { get; private set; }

    private void Start()
    {
        if (startOpened) OpenGate();
    }

    public void OpenGate()
    {
        animator.Play(openAnim);
        IsOpen = true;
    }

    public void CloseGate()
    {
        animator.Play(closeAnim);
        IsOpen = false;
    }
}
