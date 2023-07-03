using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultsDisplayPlayers : MonoBehaviour
{
    [SerializeField] private GameObject winner;
    [SerializeField] private GameObject loser;

    private const float X_POS_PADDING = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        TeamData winner = GameResults.RoundWinner;
        if (winner == null) return;

        PlayerData[] players = GameManager.Instance.GetPlayers();

        int playerCount = 0;
        int winnerCount = 0;
        for(int i = 0; i < players.Length; i++)
        {
            if(players[i] != null)
            {
                playerCount++;
                if (players[i].PlayerTeam == winner)
                {
                    winnerCount++; 
                }
            }
        }

        GamemodeData gamemode = GameManager.Instance.GetCurrentGamemode();
        int numOfTeams = GameManager.Instance.GetCurrentTeams().Length;
        float maxDistance = (Screen.width / ((gamemode.MaxTeams + gamemode.MinTeams) - numOfTeams)) * X_POS_PADDING;

        DisplayPlayers(playerCount, winnerCount, maxDistance);
    }

    private void DisplayPlayers(float numOfPlayers, float numOfWinners, float maxDistance)
    {
        for (float i = 0; i < numOfPlayers; i++)
        {
            GameObject model = i < numOfWinners ? winner : loser;

            float xPos = Mathf.Lerp(maxDistance, -maxDistance, i / (numOfPlayers - 1));

            GameObject player = Instantiate(model, transform);
            player.transform.localPosition += new Vector3(xPos, 0, Random.Range(-1, 1));
        }
    }
}
