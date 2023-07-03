using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    [System.Serializable]
    protected class GamemodeCreator
    {
        public GamemodeRef GamemodeRef;
        public Gamemode Gamemode;
    }

    [SerializeField] protected GamemodeCreator[] gamemodes;
    protected Gamemode currentGamemode;
    [SerializeField] private bool startGameAutomatically = false;

    public bool EggsCanSpawn = true;

    public List<Basket> GetBaskets() { return baskets; }
    [Space] [SerializeField] protected List<Basket> baskets;

    public List<EggSpawner> GetEggSpawners() { return eggSpawners; }
    protected List<EggSpawner> eggSpawners;

    public Vector3 GetCenterPos() { return centerPos.position; }
    [SerializeField] protected Transform centerPos;

    private const string sceneOnComplete = "Results";
    private const string sceneOnEndEarly = "Play";

    private void Start()
    {
        if (startGameAutomatically) StartGame();

        //Display gamemode details
        currentGamemode = GetCurrentGamemodeScript();
        currentGamemode.DisplayDetails();
        
        //Collect level map data
        FindBaskets();
        eggSpawners = new List<EggSpawner>(FindObjectsOfType<EggSpawner>());

    }

    private Gamemode GetCurrentGamemodeScript()
    {
        GamemodeRef playingMode = GameManager.Instance.GetLevelSettings().GetGamemode();
        for (int i = 0; i < gamemodes.Length; i++)
        {
            if (gamemodes[i].GamemodeRef == playingMode)
            {
                return gamemodes[i].Gamemode;
            }
        }

        return null;
    }

    public void StartGame()
    {
        StartCoroutine(LevelLoop());
    }

    protected IEnumerator LevelLoop()
    {
        float duration = GameManager.Instance.GetLevelSettings().GetRoundDuration();
        currentGamemode.gameObject.SetActive(true);
        
        GameUIManager.Instance.StartTimer(duration, 10f);
        yield return new WaitForSeconds(duration);

        currentGamemode.EndGamemode();
        currentGamemode.gameObject.SetActive(false);
    }

    private void FindBaskets()
    {
        TeamData[] teams = GameManager.Instance.GetActiveTeams();

        //Do not mess with baskets if active teams are not assigned
        if(teams.Length > 0)
        {
            //Remove extra baskets from active teams
            for (int i = baskets.Count - 1; i > teams.Length -1; i--)
            {
                Destroy(baskets[i].gameObject);
                baskets.RemoveAt(i);
            }

            //Set baskets to teams
            for (int i = 0; i < teams.Length; i++)
            {
                baskets[i].SetTeam(teams[i]);
            }
        }
    }

    public Basket GetBasket(TeamData team)
    {
        for(int i = 0; i < baskets.Count; i++)
        {
            if (baskets[i].GetTeam() == team) return baskets[i];
        }

        return null;
    }

    public List<EggSpawner> GetEggSpawnersOfType(EggType type)
    {
        List<EggSpawner> spawnersOfType = new List<EggSpawner>();

        for(int i = 0; i < eggSpawners.Count; i++)
        {
            if(eggSpawners[i].GetEggType() == type) spawnersOfType.Add(eggSpawners[i]);
        }

        return spawnersOfType;
    }

    public void EndGame(bool quit = false)
    {
        //Winners are determined in Gamemode class
        StopAllCoroutines();
        StartCoroutine(EndingGame(quit));
    }

    private IEnumerator EndingGame(bool quit)
    {
        Time.timeScale = 0;
        GameUIManager.Instance.CanPause = false;
        GameManager.Instance.StopBGM();
        
        yield return new WaitForSecondsRealtime(5f);

        Time.timeScale = 1;
        GameUIManager.Instance.CanPause = true;

        if (quit) SceneManager.LoadScene(sceneOnEndEarly);
        else SceneManager.LoadScene(sceneOnComplete);
    }
}
