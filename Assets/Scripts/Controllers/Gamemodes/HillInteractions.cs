using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HillInteractions : MonoBehaviour, ITeamReferencer
{
    [SerializeField] private GameObject hillBasketPrefab;
    [SerializeField] private float scoreInterval;

    private List<TeamData> teams = new List<TeamData>();
    private List<Player> players = new List<Player>();

    public List<Basket> GetHillBaskets() { return baskets.Values.ToList(); }
    private Dictionary<string, Basket> baskets = new Dictionary<string, Basket>();

    public delegate void OnOwnerChangeDelegate(TeamData owner);
    public OnOwnerChangeDelegate OnOwnerChange;

    TeamData hillOwner = null;

    private void Start()
    {
        TeamData[] teams = GameManager.Instance.GetActiveTeams();
        for(int i = 0; i < teams.Length; i++)
        {
            Basket teamBasket = Instantiate(hillBasketPrefab, transform).GetComponent<Basket>();
            teamBasket.SetTeam(teams[i]);
            baskets.Add(teams[i].TeamName, teamBasket);
        }

        SetTeam(null);
    }

    public TeamData GetTeam()
    {
        return hillOwner;
    }

    public void SetTeam(TeamData team)
    {
        if (team != null && team == hillOwner) return;

        hillOwner = team;
        OnOwnerChange?.Invoke(hillOwner);
        StopAllCoroutines();
        
        if(hillOwner != null) StartCoroutine(AddToTeamBasket());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponentInParent<Player>();
        if(player && !players.Contains(player))
        {
            players.Add(player);
            UpdateTeams();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponentInParent<Player>();
        if (player && players.Contains(player))
        {
            players.Remove(player);
            UpdateTeams();
        }
    }

    private void UpdateTeams()
    {
        List<TeamData> newTeams = new List<TeamData>();

        for (int i = 0; i < players.Count; i++)
        {
            if (!newTeams.Contains(players[i].GetTeam()))
            {
                newTeams.Add(players[i].GetTeam());
            }
        }

        teams = newTeams;

        //If there are no teams or more than one, there is no owner assigned
        if (teams.Count == 0 || teams.Count > 1) SetTeam(null);
        else SetTeam(teams[0]);
    }

    private IEnumerator AddToTeamBasket()
    {
        while(hillOwner != null)
        {
            yield return new WaitForSeconds(scoreInterval);

            if(baskets.ContainsKey(hillOwner.TeamName)) baskets[hillOwner.TeamName].AddToPocket(1);
        }
    }
}
