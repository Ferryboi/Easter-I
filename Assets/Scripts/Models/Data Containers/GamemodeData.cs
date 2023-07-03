using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGamemodeData", menuName = "Data/Gamemodes/Gamemode")]
public class GamemodeData : ScriptableObject
{
    public GamemodeRef GamemodeRef => gamemodeRef;
    [SerializeField] private GamemodeRef gamemodeRef;

    public string Title => title;
    [Space] [SerializeField] private string title;

    public string Description => description;
    [SerializeField] private string description;

    public string PrimaryText => primaryText;
    [Space][SerializeField] private string primaryText;

    public string SubText => subText;
    [SerializeField] private string subText;

    public Sprite Image => image;
    [Space][SerializeField] private Sprite image;

    public int MinTeams => minTeams;
    [Space][SerializeField] private int minTeams;

    public int MaxTeams => maxTeams;
    [SerializeField] private int maxTeams;

    public TeamData[] Teams => teams;
    [SerializeField] private TeamData[] teams;
}
