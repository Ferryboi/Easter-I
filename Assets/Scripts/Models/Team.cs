using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Team
{
    public string GetTeamRef() { return teamRef; }
    [SerializeField] private string teamRef;

    public Color GetTeamColor() { return teamColor; }
    [SerializeField] public Color teamColor;

    /*
    public bool AddPlayerToTeam(PlayerData player)
    {
        if (player.PlayerTeam != this)
        {
            player.SetPlayerTeam(this);
            teamCount++;

            Debug.Log(player.PlayerRef + " has joined " + teamRef + ". Total team members is now " + teamCount);
            return true;
        }

        return false;
    }

    public bool RemovePlayerFromTeam(PlayerData player)
    {
        if (player.PlayerTeam == this)
        {
            player.SetPlayerTeam(null);
            teamCount--;

            Debug.Log(player.PlayerRef + " has left " + teamRef + ". Total team members is now " + teamCount);
            return true;
        }

        return false;
    }

    public void ResetTeam()
    {
        Debug.Log(teamRef + " has been reset from " + teamCount + " to 0");
        teamCount = 0;
    }
    */

    public int GetPlayerCount() { return teamCount; }
    [NonSerialized] private int teamCount = 0;
}
