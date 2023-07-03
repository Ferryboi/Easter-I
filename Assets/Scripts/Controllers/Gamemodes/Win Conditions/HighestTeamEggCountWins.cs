using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighestTeamEggCountWins : WinCondition
{
    [SerializeField] protected bool includePlayerPockets = true;
    [SerializeField] protected bool includeBaskets = true;

    [Space]
    [SerializeField] protected bool includeDeathPenalty = false;

    public override void DetermineWinner()
    {
        Dictionary<TeamData, int> totalScores = new Dictionary<TeamData, int>();
        List<PlayableCharacter> players = PlayerManager.Instance.Players;

        //Talley eggs held by players
        if (includePlayerPockets)
        {
            for (int i = 0; i < players.Count; i++)
            {
                TeamData playerTeam = players[i].GetTeam();
                Player player = (Player)players[i];

                if (totalScores.ContainsKey(playerTeam))
                {
                    totalScores[playerTeam] += player.GetPocket().EmptyPocket();
                }
                else
                {
                    totalScores.Add(playerTeam, player.GetPocket().EmptyPocket());
                }
            }
        }


        //Talley eggs held in baskets
        if (includeBaskets)
        {
            Basket[] baskets = FindObjectsOfType<Basket>();
            for (int i = 0; i < baskets.Length; i++)
            {
                TeamData basketTeam = baskets[i].GetTeam();

                if (totalScores.ContainsKey(basketTeam))
                {
                    totalScores[basketTeam] += baskets[i].EmptyPocket();
                }
                else
                {
                    totalScores.Add(basketTeam, baskets[i].EmptyPocket());
                }
            }
        }

        Dictionary<TeamData, int> deaths = new Dictionary<TeamData, int>();
        Dictionary<TeamData, int> finalScores = new Dictionary<TeamData, int>();

        if (includeDeathPenalty)
        {
            for (int i = 0; i < players.Count; i++)
            {
                TeamData playerTeam = players[i].GetTeam();
                Player player = (Player)players[i];
                if (playerTeam)
                {
                    int deathScore = player.GetHealth().GetNumOfDeaths() * PlayerDeath.RESPAWN_COST;
                    if (deaths.ContainsKey(playerTeam))
                    {
                        deaths[playerTeam] += deathScore;

                        if (totalScores.ContainsKey(playerTeam)) finalScores[playerTeam] = totalScores[playerTeam] - deaths[playerTeam];
                        else finalScores[playerTeam] = 0;
                    }
                    else
                    {
                        deaths.Add(playerTeam, deathScore);

                        if (totalScores.ContainsKey(playerTeam)) finalScores.Add(playerTeam, totalScores[playerTeam] - deaths[playerTeam]);
                        else finalScores.Add(playerTeam, 0);
                    }
                }
            }
        }
        else
        {
            finalScores = totalScores;
        }


        //Determine winner based on scores
        TeamData winner = null;
        int highestScore = int.MinValue;
        foreach(var teamScore in finalScores)
        {
            if (teamScore.Value > highestScore)
            {
                winner = teamScore.Key;
                highestScore = teamScore.Value;
            }
            else if(teamScore.Value == highestScore)
            {
                winner = null;
            }
        }

        if (includeDeathPenalty) OnWinnerDetermined?.Invoke(winner, finalScores, totalScores, deaths);
        else OnWinnerDetermined?.Invoke(winner, finalScores, null, null);
    }
}
