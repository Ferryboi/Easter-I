using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSpeedOverTime : MonoBehaviour
{
    [SerializeField] private Movement movement;
    [SerializeField] private float increaseRate;

    // Update is called once per frame
    void Update()
    {
        movement.SetSpeed(movement.GetSpeed() * increaseRate * Time.deltaTime);
    }
}
