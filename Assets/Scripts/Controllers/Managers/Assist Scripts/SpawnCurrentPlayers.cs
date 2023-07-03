using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCurrentPlayers : MonoBehaviour
{
    private bool started = false;

    private void Start()
    {
        EnablePlayers();
        started = true;
    }

    private void OnEnable()
    {
        if (started) EnablePlayers();
    }

    private void OnDisable()
    {
        //THIS IS CAUSING DUPLICATE PLAYERMANAGER WHEN CHANGING SCENES

        //PlayerManager.Instance.DestroyActivePlayers();
        //PlayerManager.Instance.GetPlayerInputManager().DisableJoining();
    }

    private void EnablePlayers()
    {
        PlayerManager.Instance.SpawnPlayers(GameManager.Instance.GetPlayers());
        PlayerManager.Instance.GetPlayerInputManager().EnableJoining();
    }
}
