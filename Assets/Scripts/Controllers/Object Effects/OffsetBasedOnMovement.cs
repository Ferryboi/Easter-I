using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetBasedOnMovement : MonoBehaviour
{
    [SerializeField] private Movement movement;
    [Space]
    [SerializeField] private float offsetMoving;
    [SerializeField] private float offsetStill;

    private void Awake()
    {
        movement.OnMovementChange += ChangeOffset;
    }

    private void OnDestroy()
    {
        movement.OnMovementChange -= ChangeOffset;
    }

    private void ChangeOffset()
    {
        if(movement.IsMoving)
        {
            transform.localPosition = Vector3.up * offsetMoving;
        }
        else
        {
            transform.localPosition = Vector3.up * offsetStill;
        }
    }
}
