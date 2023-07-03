using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WinCondition : MonoBehaviour
{
    public delegate void OnWinnerDelegate(TeamData winner, Dictionary<TeamData, int> scores, Dictionary<TeamData, int> deaths, Dictionary<TeamData, int> finalScores, bool timeOut = true);
    public OnWinnerDelegate OnWinnerDetermined;

    public abstract void DetermineWinner();

    protected void PredetermineWinner(TeamData winner, Dictionary<TeamData, int> finalScores, Dictionary<TeamData, int> scores = null, Dictionary<TeamData, int> deaths = null)
    {
        OnWinnerDetermined?.Invoke(winner, finalScores, scores, deaths, false);
        StopAllCoroutines();
    }
}
