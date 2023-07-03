using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResults
{
    public static TeamData RoundWinner { get; private set; }

    public static Dictionary<TeamData, int> FinalScores { get; private set; }
    public static Dictionary<TeamData, int> TotalScores { get; private set; }
    public static Dictionary<TeamData, int> DeathScores { get; private set; }

    public static void ResetAllScores()
    {
        FinalScores = new Dictionary<TeamData, int>();
        TotalScores = new Dictionary<TeamData, int>();
        DeathScores = new Dictionary<TeamData, int>();
        RoundWinner = null;
    }

    public static void SetGameResults(TeamData winner, Dictionary<TeamData, int> finalScores, Dictionary<TeamData, int> totalScores, Dictionary<TeamData, int> deathScores)
    {
        RoundWinner = winner;
        FinalScores = finalScores;
        TotalScores = totalScores;
        DeathScores = deathScores;
    }
}
