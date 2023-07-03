using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateToggle : MonoBehaviour
{
    [SerializeField] private Gate[] gates;
    [SerializeField] private float minInterval;
    [SerializeField] private float maxInterval;
    [SerializeField] private float chanceToToggle;

    public void Start()
    {
        if(gates.Length != 0) StartCoroutine(ToggleGates());
    }

    private IEnumerator ToggleGates()
    {
        while(true)
        {
            for(int i = 0; i < gates.Length; i++)
            {
                //If gate is currently open, it is guaranteed to close during next iteration
                if (gates[i].IsOpen)
                {
                    gates[i].CloseGate();
                }
                //Has a chance for gate to open
                else if (Random.Range(0, 100) < chanceToToggle)
                {
                    gates[i].OpenGate();
                }
            }

            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
        }
    }
}
