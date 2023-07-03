using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemode : MonoBehaviour
{
    public GamemodeData GetGamemodeData() { return gamemodeData; }
    [SerializeField] private GamemodeData gamemodeData;

    public WinCondition GetWinCondition() { return winCondition; }
    [SerializeField] private WinCondition winCondition;

    [Space]
    [SerializeField] private AudioClip overrideMusic;

    [Space]
    [SerializeField] private GameObject centerPrefab;

    // Start is called before the first frame update
    void Start()
    {
        winCondition.OnWinnerDetermined += GameEnded;
    }

    private void OnDestroy()
    {
        winCondition.OnWinnerDetermined -= GameEnded;
    }

    public void DisplayDetails()
    {
        GameUIManager.Instance.DisplayText(15, 1, gamemodeData.PrimaryText, gamemodeData.SubText);

        if(overrideMusic != null)
        {
            PlayBGM bgMusicSetter = FindObjectOfType<PlayBGM>();
            bgMusicSetter.SetMusic(overrideMusic);
            bgMusicSetter.PlayMusic();
        }

        if (centerPrefab != null)
        {
            Vector3 pos = LevelManager.Instance.GetCenterPos();

            Instantiate(centerPrefab, pos, Quaternion.identity);
        }
    }

    public void EndGamemode()
    {
        winCondition.DetermineWinner();
    }

    public void GameEnded(TeamData winner, Dictionary<TeamData, int> finalScores, Dictionary<TeamData, int> totalScores, Dictionary<TeamData, int> deathScores, bool timeOut)
    {
        string winText = timeOut ? "Time!" : winner != null ? "We have a Winner!" : "Tie!";
        GameUIManager.Instance.DisplayText(5, 1, winText);

        GameResults.SetGameResults(winner, finalScores, totalScores, deathScores);
        LevelManager.Instance.EndGame();
        Destroy(gameObject);
    }
}
