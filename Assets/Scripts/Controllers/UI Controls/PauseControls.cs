using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControls : MonoBehaviour
{
    public void ContinueGame()
    {
        GameUIManager.Instance.TogglePause();
    }

    public void EndGame()
    {
        GameUIManager.Instance.TogglePause();
        GameUIManager.Instance.DisplayText(5f, 1, "Game Ended");

        LevelManager.Instance.EndGame(true);
    }
}
