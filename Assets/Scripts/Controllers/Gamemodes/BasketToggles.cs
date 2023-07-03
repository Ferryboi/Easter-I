using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketToggles : MonoBehaviour
{
    [SerializeField] private bool canInteractOwner;
    [SerializeField] private bool canInteractEnemy;

    [Space]
    [SerializeField] private bool basketPickup;
    [SerializeField] private bool dropOnDeath;

    private void Start()
    {
        BasketInteraction.CanInteractOwner = canInteractOwner;
        BasketInteraction.CanInteractEnemy = canInteractEnemy;

        BasketInteraction.CanPickup = basketPickup;
        PlayerDeath.DropBasketOnDeath = dropOnDeath;
    }
}
