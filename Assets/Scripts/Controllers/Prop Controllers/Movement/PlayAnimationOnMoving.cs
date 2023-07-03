using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationOnMoving : MonoBehaviour
{
    [SerializeField] private Movement movement;
    [Space]
    [SerializeField] private Animator animator;
    [SerializeField] private string movingBool;

    private void Awake()
    {
        movement.OnMovementChange += PlayAnim;
    }

    private void OnDestroy()
    {
        movement.OnMovementChange -= PlayAnim;
    }

    private void PlayAnim()
    {
        animator.SetBool(movingBool, movement.IsMoving);
    }
}
