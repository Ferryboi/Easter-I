using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTeamBasedOnResults : MonoBehaviour
{
    [SerializeField] private Image[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        ChangeToTeam(GameResults.RoundWinner);
    }

    private void ChangeToTeam(TeamData team)
    {
        Color color = team == null ? Color.white : team.Color;

        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].color = color;
        }
    }
}
