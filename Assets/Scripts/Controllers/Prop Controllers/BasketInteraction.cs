using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private Basket basket;
    private List<Player> players = new List<Player>();

    public const float COOLDOWN_INTERVAL = 0.25f;
    private bool onCooldown = false;

    [Space]
    [SerializeField] public static bool CanPickup = false;
    [SerializeField] private int ownerTakeEggCount = 5;

    [Space]
    public static bool CanInteractOwner = true;
    public static bool CanInteractEnemy = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponentInParent<Player>();
        if(player)
        {
            if(player.GetTeam() == basket.GetTeam() && CanInteractOwner) player.GetPlayerActions().SetInteractable(this);
            else if(player.GetTeam() != basket.GetTeam() && CanInteractEnemy) player.GetPlayerActions().SetInteractable(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponentInParent<Player>();
        if (player)
        {
            player.GetPlayerActions().RemoveInteractable(this);
            OnInteractHoldEnd(player);
        }
    }

    //Player count is dictated in InteractHold methods
    private void Update()
    {
        if (!onCooldown && players.Count > 0)
        {
            PlayersStealEggs();
        }
    }

    public void OnInteract(Player player)
    {
        //Do nothing on interact
    }

    public void OnInteractHoldEnd(Player player)
    {
        if (players.Contains(player)) players.Remove(player);
    }

    public void OnInteractHoldStart(Player player)
    {
        if(player.GetTeam() == basket.GetTeam())
        {
            if (CanPickup) OwnerCollectBasket(player);
            else OwnerStoreEggs(player);
            return;
        }

        if (!players.Contains(player)) players.Add(player);
    }

    private void OwnerStoreEggs(Player player)
    {
        bool playerHasEggs = player.GetPocket().GetEggCount() > 0;
        if(playerHasEggs)
        {
            basket.AddToPocket(player.GetPocket().EmptyPocket());
        }
        else
        {
            player.GetPocket().AddToPocket(basket.RemoveFromPocket(ownerTakeEggCount));
        }
    }

    private void OwnerCollectBasket(Player player)
    {
        player.GetPocket().AddToPocket(basket.EmptyPocket());

        player.GetPlayerActions().RemoveInteractable(this);
        RemoveBasket();
    }

    private void PlayersStealEggs()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetPocket().AddToPocket(basket.RemoveFromPocket(1));
        }

        if(basket.GetEggCount() > 0)
        {
            StartCoroutine(Cooldown(COOLDOWN_INTERVAL));
        }
        else if (CanPickup)
        {
            RemoveBasket();
        }
    }

    private IEnumerator Cooldown(float cooldown)
    {
        onCooldown = true;

        yield return new WaitForSeconds(cooldown);

        onCooldown = false;
    }

    private void RemoveBasket()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetPlayerActions().RemoveInteractable(this);
        }

        Destroy(basket.gameObject);
    }
}
