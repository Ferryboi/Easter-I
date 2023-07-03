using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeTagControls : MonoBehaviour
{
    [SerializeField] private GameObject freezer;
    [SerializeField] private AllPlayersFrozen freezeTagWinCondition;

    [Space][SerializeField] private string nameOfSeekerTeam = "Seekers";

    public TeamData AttackingTeam { get; private set; }

    private void Start()
    {
        TeamData[] teams = GameManager.Instance.GetCurrentTeams();

        //Setting up the seekers
        AttackingTeam = GetAttackingTeam(teams);
        freezeTagWinCondition.SetAttackingTeam(AttackingTeam);

        //Set up players. Override all players OnDeath with freeze controls
        //Set up hiders to not be able to attack
        //Set up seekers to have infinite ammo and be invincible
        List<PlayableCharacter> players = PlayerManager.Instance.Players;
        for (int i = 0; i < players.Count; i++)
        {
            Player player = (Player)players[i];
            
            if (players[i].GetTeam() != AttackingTeam)
            {
                player.GetPlayerActions().CanAttack = false;
            }    
            else
            {
                player.GetPlayerActions().InfiniteAmmo = true;
            }
        }

        PlayerHealth.OverrideDeathController = true;
        PlayerHealth.OnDeath += PlayerHit;

        //Disable all egg spawners
        LevelManager.Instance.EggsCanSpawn = false;

        Egg[] eggs = FindObjectsOfType<Egg>();
        for(int i = 0; i < eggs.Length; i++)
        {
            Destroy(eggs[i].gameObject);
        }
    }

    private void OnDestroy()
    {
        PlayerHealth.OverrideDeathController = false;
        PlayerHealth.OnDeath -= PlayerHit;

        if (PlayerManager.Instance != null)
        {
            List<PlayableCharacter> players = PlayerManager.Instance.Players;
            for (int i = 0; i < players.Count; i++)
            {
                Player player = (Player)players[i];

                if (players[i].GetTeam() != AttackingTeam)
                {
                    player.GetPlayerActions().CanAttack = true;
                }
                else
                {
                    player.GetPlayerActions().InfiniteAmmo = false;
                }
            }
        }
    }

    private void PlayerHit(Player player)
    {
        player.GetHealth().HealPlayer(player.GetHealth().GetMaxHealth());

        Instantiate(freezer, player.transform);
        freezeTagWinCondition.CheckIfAllPlayersFrozen();
    }

    private TeamData GetAttackingTeam(TeamData[] teams)
    {
        for(int i = 0; i < teams.Length; i++)
        {
            if (teams[i].TeamName == nameOfSeekerTeam) return teams[i];
        }

        if (teams.Length > 0) return teams[0];
        return null;
    }
}
