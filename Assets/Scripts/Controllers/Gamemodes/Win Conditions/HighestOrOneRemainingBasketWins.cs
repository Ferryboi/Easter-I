using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighestOrOneRemainingBasketWins : HighestTeamEggCountWins
{
    private bool keepCheckingBaskets = true;

    private void Update()
    {
        if (!keepCheckingBaskets) return;

        List<Basket> baskets = LevelManager.Instance.GetBaskets();

        for(int i = baskets.Count - 1; i >= 0; i--)
        {
            if (baskets[i] == null) baskets.RemoveAt(i);
        }

        CheckForWinner(baskets);
    }

    private void CheckForWinner(List<Basket> baskets)
    {
        if (baskets.Count == 0)
        {
            keepCheckingBaskets = false;
            PredetermineWinner(null, null);
        }
        else if (baskets.Count == 1)
        {
            Dictionary<TeamData, int> scores = new Dictionary<TeamData, int>();
            TeamData[] teams = GameManager.Instance.GetActiveTeams();
            TeamData winner = baskets[0].GetTeam();

            //Create dictionary where all players have score 0 except winners
            for (int i = 0; i < teams.Length; i++)
            {
                if (teams[i] == winner)
                {
                    scores.Add(winner, baskets[0].EmptyPocket());
                }
                else
                {
                    scores.Add(teams[i], 0);
                }
            }

            keepCheckingBaskets = false;
            PredetermineWinner(winner, scores);
        }
    }
}
