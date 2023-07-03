using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostAlivePlayers : WinCondition
{
    private List<Player> players;

    public override void DetermineWinner()
    {
        Dictionary<TeamData, int> scores = GetScores();

        TeamData winner = null;
        int winnerScore = int.MinValue;
        foreach(var score in scores)
        {
            if(score.Value > winnerScore)
            {
                winner = score.Key;
                winnerScore = score.Value;
            }
            else if(score.Value == winnerScore)
            {
                winner = null;
            }
        }

        OnWinnerDetermined?.Invoke(winner, scores, null, null);
    }

    private void Start()
    {
        List<PlayableCharacter> pChars = PlayerManager.Instance.Players;
        players = new List<Player>();

        for(int i = 0; i < pChars.Count; i++)
        {
            players.Add((Player)pChars[i]);
        }

        PlayerHealth.OnDeath += CheckIfAllButOneTeamDead;
    }

    private void OnDestroy()
    {
        PlayerHealth.OnDeath -= CheckIfAllButOneTeamDead;
    }

    private void CheckIfAllButOneTeamDead(Player player)
    {
        Dictionary<TeamData, int> scores = GetScores();

        TeamData winner = null;
        int numTeamsAlive = 0;
        foreach(var score in scores)
        {
            if(score.Value > 0)
            {
                winner = score.Key;
                numTeamsAlive++;
            }
        }

        if (numTeamsAlive == 1) PredetermineWinner(winner, scores);
    }

    private Dictionary<TeamData, int> GetScores()
    {
        Dictionary<TeamData, int> scores = new Dictionary<TeamData, int>();

        TeamData[] teams = GameManager.Instance.GetActiveTeams();
        for (int i = 0; i < teams.Length; i++)
        {
            scores.Add(teams[i], 0);
        }

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].GetHealth().GetHealth() > 0) scores[players[i].GetTeam()]++;
        }

        return scores;
    }
}
