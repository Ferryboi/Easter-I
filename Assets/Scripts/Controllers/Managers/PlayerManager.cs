using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : Singleton<PlayerManager>
{
    public PlayerInputManager GetPlayerInputManager() { return playerInputManager; }
    [SerializeField] protected PlayerInputManager playerInputManager;

    public List<PlayableCharacter> Players { get; protected set; }
    [Space]
    [SerializeField] protected GameObject playerPrefab;
    [SerializeField] protected Transform[] spawnPositions;

    [Space]
    public bool CanSpawn;
    public bool CanRespawn;

    [Space]
    [SerializeField] private bool SpawnOnStart = true;

    public delegate void OnPlayerChangeDelegate(PlayableCharacter player);
    public OnPlayerChangeDelegate OnPlayerAdded;
    public OnPlayerChangeDelegate OnPlayerLeaving;


    private void Awake()
    {
        Players = new List<PlayableCharacter>();
    }

    private void Start()
    {
        if (SpawnOnStart) StartGame();
        GameManager.Instance.OnPlayerChange += OnPlayerChange;
    }

    public void StartGame()
    {
        SpawnPlayers(GameManager.Instance.GetPlayers());
    }

    private void OnDestroy()
    {
        if(GameManager.Instance) GameManager.Instance.OnPlayerChange -= OnPlayerChange;
        DestroyInstance();
    }

    protected void OnPlayerChange(PlayerData data, PlayerJoinAction action)
    {
        StartCoroutine(DelayedAction(data, action));

        //Need to delay the action so the player creator can delete itself, otherwise player creator acts as P1 and newly spawned player acts as P2
        IEnumerator DelayedAction(PlayerData data, PlayerJoinAction action)
        {
            yield return 0;

            switch (action)
            {
                case PlayerJoinAction.Add:
                    SpawnPlayer(data);
                    break;
                case PlayerJoinAction.Remove:
                    DestroyPlayer(data.PlayerRef);
                    break;
            }
        }
    }

    public PlayableCharacter SpawnPlayer(PlayerData data)
    {
        if (!CanSpawn) return null;

        for(int i = 0; i < Players.Count; i++)
        {
            if (Players[i].GetPlayerRef() == data.PlayerRef)
            {
                return null;
            }
        }

        //Create a new player and assign details
        PlayableCharacter newPlayer = PlayerInput.Instantiate(playerPrefab, controlScheme: data.Input.currentControlScheme, pairWithDevices: data.Devices).GetComponent<Player>();
        newPlayer.SetPlayerRef(data.PlayerRef);
        newPlayer.SetTeam(data.PlayerTeam);
        newPlayer.transform.position = GetPlayerSpawnPos(data.PlayerTeam);
        
        Players.Add(newPlayer);
        OnPlayerAdded?.Invoke(newPlayer);
        return newPlayer;
    }

    public List<PlayableCharacter> SpawnPlayers(PlayerData[] datas)
    {
        if (!CanSpawn) return Players;

        for(int i = 0; i < datas.Length; i++)
        {
            if(datas[i] != null)
            {
                SpawnPlayer(datas[i]);
            }
        }

        return Players;
    }

    public bool DestroyPlayer(PlayerRef playerRef)
    {
        for(int i = 0; i < Players.Count; i++)
        {
            if(Players[i].GetPlayerRef() == playerRef)
            {
                if (Players[i])
                {
                    OnPlayerLeaving?.Invoke(Players[i]);
                    Destroy(Players[i].gameObject);
                }
                Players.RemoveAt(i);
                return true;
            }
        }

        return false;
    }

    public bool DestroyActivePlayers()
    {
        if (Players.Count == 0) return false;

        for(int i = Players.Count - 1; i >= 0; i--)
        {
            if(Players[i]) Destroy(Players[i].gameObject);
            Players.RemoveAt(i);
        }

        return true;
    }

    public Vector3 GetPlayerSpawnPos(TeamData team)
    {
        if (spawnPositions.Length == 0) return Vector3.zero;
        if (spawnPositions.Length == 1) return spawnPositions[0].position;

        TeamData[] teams = GameManager.Instance.GetActiveTeams();

        for(int i = 0; i < teams.Length; i++)
        {
            if(team == teams[i])
            {
                return spawnPositions[i % spawnPositions.Length].position;
            }
        }

        //If somehow get here, return random position
        return spawnPositions[Random.Range(0, spawnPositions.Length)].position;
    }
}
