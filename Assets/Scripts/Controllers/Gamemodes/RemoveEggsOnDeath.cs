using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveEggsOnDeath : MonoBehaviour
{
    [SerializeField] private int eggsToRemove = 0;

    // Start is called before the first frame update
    void Start()
    {
        PlayerHealth.OnDeath += RemoveEggsFromBasket;
    }

    private void OnDestroy()
    {
        PlayerHealth.OnDeath -= RemoveEggsFromBasket;
    }

    private void RemoveEggsFromBasket(Player player)
    {
        Basket basket = LevelManager.Instance.GetBasket(player.GetTeam());

        if(basket != null)
        {
            basket.RemoveFromPocket(eggsToRemove);
        }
    }
}
