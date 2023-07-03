using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTeamGroup", menuName = "Data/Teams")]
public class TeamGroup : ScriptableObject
{
    public int GetMaxTeams() { return maxTeams; }
    [SerializeField] private int maxTeams = 4;
    public int GetMinTeams() { return minTeams; }
    [SerializeField] private int minTeams = 2;
    
    [Space]
    [SerializeField] private Team[] teams;

    /*
    public Team[] GetTeams() { return teams; }

    public Team GetTeam(string team)
    {
        for(int i = 0; i < teams.Length; i++)
        {
            if(teams[i].GetTeamRef() == team)
            {
                return teams[i];
            }
        }

        return null;
    }

    public List<Team> GetActiveTeams()
    {
        List<Team> activeTeams = new List<Team>();

        for(int i = 0; i < teams.Length; i++)
        {
            if(teams[i].GetPlayerCount() > 0)
            {
                activeTeams.Add(teams[i]);
            }
        }

        return activeTeams;
    }

    public List<Team> GetInactiveTeams()
    {
        List<Team> inactiveTeams = new List<Team>();

        for(int i = 0; i < teams.Length; i++)
        {
            if(teams[i].GetPlayerCount() == 0)
            {
                inactiveTeams.Add(teams[i]);
            }
        }

        return inactiveTeams; 
    }
    */
}
