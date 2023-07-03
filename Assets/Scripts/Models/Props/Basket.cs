using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : Pocket, ITeamReferencer
{
    private TeamData team;

    public delegate void OnTeamChangeDelegate(Color color);
    public OnTeamChangeDelegate OnTeamChange;

    public delegate void OnBasketEmptyDelegate(Basket basket);
    public static OnBasketEmptyDelegate OnBasketEmptied;
    public static OnBasketEmptyDelegate OnBasketFirstAdded;

    [SerializeField] private bool usePresetTeam;
    [SerializeField] private TeamData presetTeam;

    public BasketInteraction GetBasketInteraction() { return basketInteraction; }
    [Space] [SerializeField] private BasketInteraction basketInteraction;

    private void Start()
    {
        if(usePresetTeam)
        {
            team = presetTeam;
        }

        OnChange += BasketEmptyCall;
    }

    private void OnDestroy()
    {
        OnChange -= BasketEmptyCall;
    }

    public void SetTeam(TeamData team)
    {
        this.team = team;

        Debug.Log(team);
        OnTeamChange?.Invoke(team.Color);
    }

    public TeamData GetTeam()
    {
        return team;
    }

    private void BasketEmptyCall(int change)
    {
        if (GetEggCount() == 0 && GetPrevEggCount() > 0) OnBasketEmptied?.Invoke(this);
        else if (GetEggCount() > 0 && GetPrevEggCount() == 0) OnBasketFirstAdded?.Invoke(this);
    }
}
