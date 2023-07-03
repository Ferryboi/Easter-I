using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HillWinnerWins : WinCondition
{
    public override void DetermineWinner()
    {
        HillInteractions hill = FindObjectOfType<HillInteractions>();

        List<Basket> baskets = hill.GetHillBaskets();

        Dictionary<TeamData, int> scores = new Dictionary<TeamData, int>();

        TeamData winner = null;
        int highestScore = int.MinValue;

        for (int i = 0; i < baskets.Count; i++)
        {
            TeamData basketTeam = baskets[i].GetTeam();
            int teamScore = baskets[i].EmptyPocket();

            if (teamScore > highestScore)
            {
                winner = baskets[i].GetTeam();
                highestScore = teamScore;
            }

            scores.Add(basketTeam, teamScore);
        }

        OnWinnerDetermined?.Invoke(winner, scores, null, null);
    }
}
