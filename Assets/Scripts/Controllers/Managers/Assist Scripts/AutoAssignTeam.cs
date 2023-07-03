using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAssignTeam : MonoBehaviour
{
    public bool AutoAssignOnJoin = false;

    private void Start()
    {
        GameManager.Instance.OnPlayerChange += AutoAssign;
    }

    private void OnDestroy()
    {
        if(GameManager.Instance) GameManager.Instance.OnPlayerChange -= AutoAssign;
    }

    public void AutoAssign(PlayerData player, PlayerJoinAction joinAction)
    {
        if (joinAction != PlayerJoinAction.Add) return;
        if (AutoAssignOnJoin == false) return;
        if (player.PlayerTeam != null) return;

        player.SetPlayerTeam(FindTeamWithLeastMembers());
    }

    public void AutoAssignAllPlayers(PlayerData[] players)
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != null && players[i].PlayerTeam != null)
            {
                players[i].SetPlayerTeam(FindTeamWithLeastMembers());
            }
        }
    }

    private TeamData FindTeamWithLeastMembers()
    {
        GamemodeData gamemode = GameManager.Instance.GetCurrentGamemode();
        TeamData[] activeTeams = GameManager.Instance.GetActiveTeams();

        if (activeTeams.Length < gamemode.MinTeams)
        {
            TeamData[] allTeams = GameManager.Instance.GetCurrentTeams();

            for (int i = 0; i < allTeams.Length; i++)
            {
                TeamData team = allTeams[i];
                bool containsTeam = false;
                for(int j = 0; j < activeTeams.Length; j++)
                {
                    if(team == activeTeams[j])
                    {
                        containsTeam = true;
                        break;
                    }
                }

                if (!containsTeam) return team;
            }

            return null;
        }
        else
        {
            PlayerData[] players = GameManager.Instance.GetPlayers();
            Dictionary<TeamData, int> teamCounts = new Dictionary<TeamData, int>();

            for(int i = 0; i < players.Length; i++)
            {
                if(players[i] != null && players[i].PlayerTeam != null)
                {
                    TeamData team = players[i].PlayerTeam;
                    if (teamCounts.ContainsKey(team)) teamCounts[team]++;
                    else teamCounts.Add(team, 1);
                }
            }

            TeamData lowestTeam = null;
            int lowestValue = int.MaxValue;

            foreach(var team in teamCounts)
            {
                if (team.Value < lowestValue)
                {
                    lowestTeam = team.Key;
                    lowestValue = team.Value;
                }
            }

            return lowestTeam;
        }
    }
}
