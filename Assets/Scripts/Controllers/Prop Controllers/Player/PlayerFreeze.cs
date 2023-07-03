using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeze : MonoBehaviour, IInteractable
{
    private Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player otherPlayer = collision.GetComponentInParent<Player>();
        if(otherPlayer)
        {
            otherPlayer.GetPlayerActions().SetInteractable(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player otherPlayer = collision.GetComponentInParent<Player>();
        if (otherPlayer)
        {
            otherPlayer.GetPlayerActions().RemoveInteractable(this);
        }
    }

    public void OnInteract(Player player)
    {
        //Nothing on interact
    }

    public void OnInteractHoldEnd(Player player)
    {
        //Nothing on interact hold end
    }

    public void OnInteractHoldStart(Player player)
    {
        if(player.GetTeam() == this.player.GetTeam())
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        player = GetComponentInParent<Player>();

        player.GetMovement().IsActive = false;
        player.GetHealth().SetInvincible(true);
        player.IsActive = false;
    }

    private void OnDestroy()
    {
        player.GetPlayerActions().RemoveInteractable(this);

        player.GetMovement().IsActive = true;
        player.GetHealth().SetInvincible(false);
        player.IsActive = true;
    }
}
