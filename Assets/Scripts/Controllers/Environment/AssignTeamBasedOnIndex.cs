using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignTeamBasedOnIndex : MonoBehaviour, ITeamReferencer
{
    [SerializeField] private int index;
    private TeamData team;

    public TeamData GetTeam()
    {
        return team;
    }

    public void SetTeam(TeamData team)
    {
        this.team = team;
    }

    // Start is called before the first frame update
    void Start()
    {
        TeamData[] activeTeams = GameManager.Instance.GetActiveTeams();

        if (index < activeTeams.Length)
        {
            SetTeam(activeTeams[index]);
            return;
        }

        List<TeamData> allTeams = new List<TeamData>(GameManager.Instance.GetCurrentTeams());

        for(int i = 0; i < activeTeams.Length; i++)
        {
            allTeams.Remove(activeTeams[i]);
        }

        if(allTeams.Count > 0) SetTeam(allTeams[index % allTeams.Count]);
        else SetTeam(activeTeams[index % activeTeams.Length]);
    }
}
