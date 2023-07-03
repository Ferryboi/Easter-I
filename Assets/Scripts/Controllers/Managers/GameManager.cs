using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public PlayerData[] GetPlayers() { return players; }
    protected PlayerData[] players = new PlayerData[4];

    public delegate void OnPlayerChangeDelegate(PlayerData player, PlayerJoinAction action);
    public OnPlayerChangeDelegate OnPlayerChange;

    public LevelSettings GetLevelSettings() { return levelSettings; }
    [SerializeField] protected LevelSettings levelSettings;

    public TeamGroup GetTeamData() { return teamData; }
    [Space] [SerializeField] protected TeamGroup teamData;

    public MapGroup GetMapData() { return mapData; }
    [SerializeField] protected MapGroup mapData;

    public GamemodeGroup GetGamemodeData() { return gamemodeData; }
    [SerializeField] protected GamemodeGroup gamemodeData;

    [Space][SerializeField] protected AudioSource bgMusic;


    private void Awake()
    {
        //If a gamemanager already exists, remove this one
        if (!SetInstance(this))
        {
            Destroy(gameObject);
        }

        transform.parent = null;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        DestroyInstance();
    }

    public PlayerRef AddPlayer(PlayerInput input)
    {
        for(int i = 0; i < players.Length; i++)
        {
            if(players[i] == null)
            {
                players[i] = new PlayerData((PlayerRef)i, input);
                OnPlayerChange?.Invoke(players[i], PlayerJoinAction.Add);
                return players[i].PlayerRef;
            }
        }

        return PlayerRef.None;
    }

    public bool RemovePlayer(PlayerRef playerRef)
    {
        int i = (int)playerRef;

        if(players[i] != null)
        {
            PlayerData oldPlayerData = players[i];
            players[i] = null;

            OnPlayerChange?.Invoke(oldPlayerData, PlayerJoinAction.Remove);
            return true;
        }

        return false;
    }

    public void RemoveAllPlayers()
    {
        for(int i = 0; i < players.Length; i++)
        {
            players[i] = null;
        }
    }

    public PlayerData GetPlayer(PlayerRef playerRef)
    {
        int i = (int)playerRef;
        if (players.Length <= i) return null;

        return players[i];
    }

    public GamemodeData GetCurrentGamemode()
    {
        return gamemodeData.GetGamemode(levelSettings.GetGamemode());
    }    

    public MapData GetCurrentMap()
    {
        return mapData.GetMap(levelSettings.GetMapType());
    }

    public TeamData[] GetCurrentTeams()
    {
        return GetCurrentGamemode().Teams;
    }

    public TeamData[] GetActiveTeams()
    {
        List<TeamData> teams = new List<TeamData>();

        for(int i = 0; i < players.Length; i++)
        {
            if(players[i] != null)
            {
                TeamData playerTeam = players[i].PlayerTeam;
                if(playerTeam != null && !teams.Contains(playerTeam)) teams.Add(playerTeam);
            }
        }

        return teams.ToArray();
    }    

    public void LoadLevel()
    {
        GameResults.ResetAllScores();
        SceneManager.LoadScene(mapData.GetMap(levelSettings.GetMapType()).SceneName);
    }

    public void PlayBGM(AudioClip clip = null)
    {
        if (clip == bgMusic.clip && bgMusic.isPlaying) return;

        if (clip != null) bgMusic.clip = clip;
        bgMusic.Play();
    }

    public void StopBGM()
    {
        bgMusic.Stop();
    }
}
