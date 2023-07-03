using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceSpeedZone : MonoBehaviour
{
    [SerializeField] private float reducePercent = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Movement movement = collision.GetComponentInParent<Movement>();
    
        if(movement)
        {
            movement.SetSpeed(movement.GetSpeed() * reducePercent);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Movement movement = collision.GetComponentInParent<Movement>();

        if(movement)
        {
            movement.SetSpeed(movement.GetSpeed() / reducePercent);
        }
    }
}
