using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITeamReferencer
{
    public void SetTeam(TeamData team);

    public TeamData GetTeam();
}
