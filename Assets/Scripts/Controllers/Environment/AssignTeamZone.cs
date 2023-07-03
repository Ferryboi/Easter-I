using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AssignTeamZone : MonoBehaviour, ITeamReferencer
{
    [SerializeField] private bool usePresetTeam;
    [SerializeField] private TeamData presetTeam;
    
    [Space][SerializeField] private bool removeTeamOnLeave;
    private TeamData team;

    public delegate void OnTeamChangeDelegate(PlayableCharacter player);
    public OnTeamChangeDelegate OnTeamChange;

    [Space][SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        if(usePresetTeam)
        {
            team = presetTeam;
        }
    }

    public void SetTeam(TeamData team)
    {
        this.team = team;
        text.text = team.TeamName;
    }

    public TeamData GetTeam()
    {
        return team;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayableCharacter player = collision.GetComponentInParent<PlayableCharacter>();
        if(player != null)
        {
            AssignPlayerToTeam(player, team);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayableCharacter player = collision.GetComponentInParent<PlayableCharacter>();
        Debug.Log(player);
        if (player != null)
        {
            AssignPlayerToTeam(player, null);
        }
    }

    private void AssignPlayerToTeam(PlayableCharacter player, TeamData team)
    {
        if (!removeTeamOnLeave && team == null) return;
        player.SetTeam(team);

        PlayerData pData = GameManager.Instance.GetPlayer(player.GetPlayerRef());
        if (pData != null)
        {
            pData.SetPlayerTeam(team);
        }

        OnTeamChange?.Invoke(player);
    }
}
