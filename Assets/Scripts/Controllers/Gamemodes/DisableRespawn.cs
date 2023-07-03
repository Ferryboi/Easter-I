using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableRespawn : MonoBehaviour
{
    private List<Player> players;

    void Start()
    {
        PlayerManager.Instance.CanRespawn = false;
    }

    private void OnDestroy()
    {
        if(PlayerManager.Instance) PlayerManager.Instance.CanRespawn = true;
    }
}
