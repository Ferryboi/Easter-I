using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnInfiniteAmmo : MonoBehaviour
{
    [SerializeField] private PlayerActions actions;
    private bool infinite;

    [Space]
    [SerializeField] private GameObject defaultDisplay;
    [SerializeField] private GameObject infiniteDisplay;

    private void Start()
    {
        infinite = actions.InfiniteAmmo;

        ToggleInfiniteDisplay(infinite);
    }

    private void Update()
    {
        if (infinite != actions.InfiniteAmmo)
        {
            infinite = actions.InfiniteAmmo;
            ToggleInfiniteDisplay(infinite);
        }
    }

    private void ToggleInfiniteDisplay(bool infinite)
    {
        defaultDisplay.SetActive(!infinite);
        infiniteDisplay.SetActive(infinite);
    }
}
