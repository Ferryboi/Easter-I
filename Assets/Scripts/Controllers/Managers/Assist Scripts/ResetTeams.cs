using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTeams : MonoBehaviour
{
    private void Start()
    {
        PlayerData[] players = GameManager.Instance.GetPlayers();

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == null) continue;

            players[i].SetPlayerTeam(null);
        }
    }
}
