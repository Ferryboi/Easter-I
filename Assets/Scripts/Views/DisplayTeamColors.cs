using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayTeamColors : MonoBehaviour
{
    private ITeamReferencer teamRef;
    [SerializeField] private Renderer[] sprites;

    private TeamData savedTeam;

    private void Start()
    {
        teamRef = GetComponentInParent<ITeamReferencer>();

        if (teamRef != null)
        {
            ChangeToTeam(teamRef.GetTeam());
        }
    }

    private void Update()
    {
        if(savedTeam != teamRef.GetTeam())
        {
            ChangeToTeam(teamRef.GetTeam());
        }
    }

    private void ChangeToTeam(TeamData team)
    {
        savedTeam = team;
        Color color = team == null ? Color.white : team.Color;

        for(int i = 0; i < sprites.Length; i++)
        {
            sprites[i].material.color = color;
        }
    }
}
