using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayableCharacter : MonoBehaviour, IPlayerReferencer, ITeamReferencer
{
    public TeamData GetTeam() { return _teamRef; }
    public void SetTeam(TeamData team) { _teamRef = team; OnTeamChange?.Invoke(team.Color); }
    protected TeamData _teamRef;

    public delegate void OnTeamChangeDelegate(Color teamColor);
    public OnTeamChangeDelegate OnTeamChange;

    public PlayerRef GetPlayerRef() { return _playerRef; }
    public void SetPlayerRef(PlayerRef playerRef) { _playerRef = playerRef; }
    protected PlayerRef _playerRef;

    [HideInInspector] public bool IsActive = true;

    public PlayerInput GetPlayerInput() { return playerInput; }

    [SerializeField] protected PlayerInput playerInput;
}
