using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BasketDisplay : PocketDisplay
{
    [Space]
    [SerializeField] protected TextMeshProUGUI teamName;

    protected override void Start()
    {
        base.Start();

        Basket basket = (Basket)pocket;
        basket.OnTeamChange += TeamChanged;

        if(basket.GetTeam() != null) teamName.text = $"{basket.GetTeam().TeamName}";
    }

    private void OnDestroy()
    {
        Basket basket = (Basket)pocket;
        basket.OnTeamChange -= TeamChanged;
    }

    private void TeamChanged(Color color)
    {
        Basket basket = (Basket)pocket;
        Debug.Log(basket);
        if (basket.GetTeam() != null) teamName.text = $"{basket.GetTeam().TeamName}";
    }
}
