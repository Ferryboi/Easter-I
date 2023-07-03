using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayHillBasket : MonoBehaviour
{
    [SerializeField] private HillInteractions hill;

    private void Awake()
    {
        hill.OnOwnerChange += OwnerChanged;
    }

    private void OnDestroy()
    {
        hill.OnOwnerChange -= OwnerChanged;
    }

    private void OwnerChanged(TeamData owner)
    {
        List<Basket> baskets = hill.GetHillBaskets();

        for(int i = 0; i < baskets.Count; i++)
        {
            if(baskets[i].GetTeam() == owner)
            {
                baskets[i].gameObject.SetActive(true);
            }
            else
            {
                baskets[i].gameObject.SetActive(false);
            }
        }
    }
}
