using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DisplayResults : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winnerDisplay;
    [SerializeField] private TextMeshProUGUI leftDisplay;
    [SerializeField] private TextMeshProUGUI middleDisplay;
    [SerializeField] private TextMeshProUGUI rightDisplay;

    private bool displayingOnlyFinalScore;

    // Start is called before the first frame update
    void Start()
    {
        if (GameResults.TotalScores == null && GameResults.DeathScores == null) displayingOnlyFinalScore = true;
        else displayingOnlyFinalScore = false;

        winnerDisplay.text = "";
        leftDisplay.text = "";
        middleDisplay.text = "";
        rightDisplay.text = "";

        StartCoroutine(Displaying());
    }

    private IEnumerator Displaying()
    {
        DisplayWinnerText();
        yield return new WaitForSeconds(5);
        DisplayScoresText();
    }

    private void DisplayWinnerText()
    {
        if(GameResults.RoundWinner != null)
        {
            winnerDisplay.text = $"The winner is {GameResults.RoundWinner.TeamName}!";
        }
        else
        {
            winnerDisplay.text = "The game ended in a Tie!";
        }
    }

    private void DisplayScoresText()
    {
        string teamScores = "Total Scores: \n";
        string deathScores = "Death Penalties: \n";
        string finalScores = "Final Scores: \n";

        if(GameResults.FinalScores != null)
        {
            foreach (var score in GameResults.FinalScores.OrderByDescending(score => score.Value))
            {
                if (displayingOnlyFinalScore)
                {
                    finalScores += $"{score.Key.TeamName}: ";
                    finalScores += $"{score.Value} \n";
                }
                else
                {
                    teamScores += $"{score.Key.TeamName}: ";

                    finalScores += $"{score.Value} \n";
                    if (GameResults.TotalScores.ContainsKey(score.Key)) teamScores += $"{GameResults.TotalScores[score.Key]} \n";
                    if (GameResults.DeathScores.ContainsKey(score.Key)) deathScores += $"-{GameResults.DeathScores[score.Key]} \n";
                }
            }
        }

        if (displayingOnlyFinalScore)
        {
            leftDisplay.text = "";
            middleDisplay.text = finalScores;
            rightDisplay.text = "";
        }
        else
        {
            leftDisplay.text = teamScores;
            middleDisplay.text = deathScores;
            rightDisplay.text = finalScores;
        }
    }
}
