using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] private PlayableCharacter player;
    [SerializeField] private Camera playerCam;

    public List<Player> DetectedPlayers { get; private set; }

    private void Start()
    {
        DetectedPlayers = new List<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player otherPlayer = collision.GetComponentInParent<Player>();
        if (otherPlayer != this.player && !DetectedPlayers.Contains(otherPlayer))
        {
            DetectedPlayers.Add(otherPlayer);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player otherPlayer = collision.GetComponentInParent<Player>();
        if (DetectedPlayers.Contains(otherPlayer))
        {
            DetectedPlayers.Remove(otherPlayer);
        }
    }

    public Player GetClosestPlayer(TeamData team = null)
    {
        Player closest = null;
        float sqrDistance = float.MaxValue;

        for(int i = 0; i < DetectedPlayers.Count; i++)
        {
            if(team == null || DetectedPlayers[i].GetTeam() == team)
            {
                Vector3 distance = transform.position - DetectedPlayers[i].transform.position;

                if(distance.sqrMagnitude < sqrDistance)
                {
                    closest = DetectedPlayers[i];
                    sqrDistance = distance.sqrMagnitude;
                }
            }
        }

        return closest;
    }

    public Player GetClosestPlayerNotInTeam(TeamData team)
    {
        Player closest = null;
        float sqrDistance = int.MaxValue;

        for (int i = 0; i < DetectedPlayers.Count; i++)
        {
            if (DetectedPlayers[i].GetTeam() != team)
            {
                Vector3 distance = transform.position - DetectedPlayers[i].transform.position;

                if (distance.sqrMagnitude < sqrDistance)
                {
                    closest = DetectedPlayers[i];
                    sqrDistance = distance.sqrMagnitude;
                }
            }
        }

        return closest;
    }
}
