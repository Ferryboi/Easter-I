using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnPlayerJoin : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private void Start()
    {
        GameManager.Instance.RemoveAllPlayers();
        GameManager.Instance.OnPlayerChange += PlayerJoined;
    }

    private void OnDestroy()
    {
        if(GameManager.Instance) GameManager.Instance.OnPlayerChange -= PlayerJoined;
    }

    private void PlayerJoined(PlayerData player, PlayerJoinAction action)
    {
        if(action == PlayerJoinAction.Add)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
