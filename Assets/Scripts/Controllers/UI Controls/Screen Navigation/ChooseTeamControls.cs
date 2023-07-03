using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseTeamControls : MonoBehaviour
{
    [SerializeField] private AssignTeamZone[] assignZones;
    private bool displayed = false;
    private bool gameStarted = false;
    private bool destroyed = false;

    private Coroutine startGameCoroutine;

    private const string displayText = "Choose Teams";
    private const string displayStartingText = "Starting Game in...";
    private const string displaySubText = "[A] / [Space] to join  |  Hold [B] / [Backspace] to leave\n[Start] / [Esc] to edit settings";

    [Space][SerializeField] private Transform teamSelectBG;

    private void Start()
    {
        DisplayTeams();
        DisplayMapBG();
        EnablePlayers();

        GameUIManager.Instance.DisplayText(float.MaxValue, 1, displayText, displaySubText);
        for (int i = 0; i < assignZones.Length; i++) assignZones[i].OnTeamChange += CheckIfPlayersReady;

        PlayerManager.Instance.OnPlayerAdded += CheckIfPlayersReady;
    }

    private void OnDestroy()
    {
        destroyed = true;
        if (GameUIManager.Instance)
        {
            GameUIManager.Instance.DeleteText();
            GameUIManager.Instance.StopTimer();
        }

        if(PlayerManager.Instance) PlayerManager.Instance.OnPlayerAdded -= CheckIfPlayersReady;

        for (int i = 0; i < assignZones.Length; i++) assignZones[i].OnTeamChange -= CheckIfPlayersReady;
    }

    private void OnEnable()
    {
        if (displayed)
        {
            GameUIManager.Instance.DisplayText(float.MaxValue, 1, displayText, displaySubText);
            DisplayTeams();
            EnablePlayers();
            DisplayMapBG();
        }
    }

    private void OnDisable()
    {
        if (gameStarted || destroyed) return;

        HideMapBG();

        for(int i = 0; i < assignZones.Length; i++)
        {
            if(assignZones[i] != null) assignZones[i].gameObject.SetActive(false);
        }

        if(PlayerManager.Instance) DisablePlayers();

        if(GameUIManager.Instance)
        {
            GameUIManager.Instance.DeleteText();
            GameUIManager.Instance.StopTimer();
        }
    }

    private void DisplayTeams()
    {
        TeamData[] teams = GameManager.Instance.GetCurrentTeams();

        for (int i = 0; i < teams.Length; i++)
        {
            if (i >= assignZones.Length) break;

            assignZones[i].gameObject.SetActive(true);
            assignZones[i].SetTeam(teams[i]);
        }

        displayed = true;
    }

    private void CheckIfPlayersReady(PlayableCharacter player)
    {
        StopStartingGame();

        if (player.GetTeam() == null) return;

        //Check if all players have an assigned team
        List<PlayableCharacter> players = PlayerManager.Instance.Players;
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].GetTeam() == null) return;
        }

        GamemodeData gamemode = GameManager.Instance.GetCurrentGamemode();

        //Check if active teams is less than minimum
        TeamData[] teams = GameManager.Instance.GetActiveTeams();
        if (teams == null || teams.Length < gamemode.MinTeams) return;

        startGameCoroutine = StartCoroutine(StartingGame());
    }

    private IEnumerator StartingGame()
    {
        GameUIManager.Instance.DeleteText();
        GameUIManager.Instance.DisplayText(1.5f, 1, displayStartingText);
        yield return new WaitForSeconds(1.5f);

        GameUIManager.Instance.StartTimer(5f, 5f);
        yield return new WaitForSeconds(5.5f);

        gameStarted = true;
        GameManager.Instance.LoadLevel();
    }

    private void StopStartingGame()
    {
        if (startGameCoroutine != null)
        {
            StopCoroutine(startGameCoroutine);
            startGameCoroutine = null;
        }

        GameUIManager.Instance.DeleteText();
        GameUIManager.Instance.DisplayText(float.MaxValue, 2, displayText, displaySubText);
        GameUIManager.Instance.StopTimer();
    }

    private void EnablePlayers()
    {
        PlayerManager.Instance.SpawnPlayers(GameManager.Instance.GetPlayers());
        PlayerManager.Instance.GetPlayerInputManager().EnableJoining();
    }

    private void DisablePlayers()
    {
        PlayerManager.Instance.DestroyActivePlayers();
        PlayerManager.Instance.GetPlayerInputManager().DisableJoining();
    }

    private void DisplayMapBG()
    {
        MapData map = GameManager.Instance.GetCurrentMap();
        Instantiate(map.TeamSelectBG, teamSelectBG);
    }

    private void HideMapBG()
    {
        if(teamSelectBG.childCount > 0)
        {
            for(int i = 0; i < teamSelectBG.childCount; i++)
            {
                Destroy(teamSelectBG.GetChild(i).gameObject);
            }
        }
    }
}
