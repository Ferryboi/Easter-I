using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPlayersFrozen : WinCondition
{
    private TeamData attackingTeam;

    public override void DetermineWinner()
    {
        TeamData[] teams = GameManager.Instance.GetActiveTeams();
        Dictionary<TeamData, int> scores = new Dictionary<TeamData, int>();
        scores.Add(attackingTeam, 0);

        TeamData winner = null;
        for (int i = 0; i < teams.Length; i++)
        {
            TeamData team = teams[i];
            if (!scores.ContainsKey(team)) scores.Add(team, 1);

            if (team != attackingTeam) winner = teams[i];
        }

        OnWinnerDetermined?.Invoke(winner, scores, null, null);
    }

    public void SetAttackingTeam(TeamData team)
    {
        attackingTeam = team;
    }

    public void CheckIfAllPlayersFrozen()
    {
        List<PlayableCharacter> players = PlayerManager.Instance.Players;

        for(int i = 0; i < players.Count; i++)
        {
            if (players[i].IsActive && players[i].GetTeam() != attackingTeam) return;
        }

        TeamData[] teams = GameManager.Instance.GetActiveTeams();
        Dictionary<TeamData, int> scores = new Dictionary<TeamData, int>();
        scores.Add(attackingTeam, 0);
        
        for(int i = 0; i < teams.Length; i++)
        {
            TeamData team = teams[i];
            if (!scores.ContainsKey(team)) scores.Add(team, 0);

            if (team != attackingTeam) scores[attackingTeam]++;
        }

        PredetermineWinner(attackingTeam, scores);
    }
}
