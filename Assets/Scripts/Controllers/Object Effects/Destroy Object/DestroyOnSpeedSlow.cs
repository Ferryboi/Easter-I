using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnSpeedSlow : MonoBehaviour
{
    [SerializeField] private Movement movement;
    private float prevSpeed = 0;

    private void LateUpdate()
    {
        if(movement.GetSpeed() < prevSpeed)
        {
            Destroy(movement.gameObject);
        }
        prevSpeed = movement.GetSpeed();
    }
}
