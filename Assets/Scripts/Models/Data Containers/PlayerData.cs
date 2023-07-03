using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerData
{
    public PlayerRef PlayerRef { get; private set; }
    public TeamData PlayerTeam { get; private set; }
    public PlayerInput Input { get; private set; }
    public InputDevice[] Devices { get; private set; }

    public PlayerData(PlayerRef playerRef, PlayerInput playerInput, TeamData playerTeam = null)
    {
        PlayerRef = playerRef;
        Input = playerInput;
        Devices = playerInput.devices.ToArray();
        PlayerTeam = playerTeam;
    }

    public void SetPlayerTeam(TeamData team)
    {
        PlayerTeam = team;
    }
}
