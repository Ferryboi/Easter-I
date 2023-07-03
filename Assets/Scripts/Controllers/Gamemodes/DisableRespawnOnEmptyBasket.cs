using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableRespawnOnEmptyBasket : MonoBehaviour
{
    List<Basket> baskets;
    List<PlayableCharacter> pChars;

    private void Start()
    {
        pChars = PlayerManager.Instance.Players;

        Basket.OnBasketEmptied += DisableRespawns;
        Basket.OnBasketFirstAdded += EnableRespawns;
    }

    private void OnDestroy()
    {
        Basket.OnBasketEmptied += DisableRespawns;
        Basket.OnBasketFirstAdded += EnableRespawns;
    }

    private void DisableRespawns(Basket basket)
    {
        TeamData team = basket.GetTeam();

        for(int i = 0; i < pChars.Count; i++)
        {
            if(pChars[i].GetTeam() == team)
            {
                Player player = (Player)pChars[i];
                player.GetHealth().CanRespawn = false;
            }
        }
    }

    private void EnableRespawns(Basket basket)
    {
        TeamData team = basket.GetTeam();

        for (int i = 0; i < pChars.Count; i++)
        {
            if (pChars[i].GetTeam() == team)
            {
                Player player = (Player)pChars[i];
                player.GetHealth().CanRespawn = true;
            }
        }
    }
}
