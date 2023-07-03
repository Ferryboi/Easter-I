using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Team", menuName = "Data/Team")]
public class TeamData : ScriptableObject
{
    public string TeamName => teamName;
    [SerializeField] private string teamName;

    public GameObject Model => model;
    [SerializeField] private GameObject model;

    public Color Color => color;
    [SerializeField] private Color color = new Color(0,0,0,255);
}
