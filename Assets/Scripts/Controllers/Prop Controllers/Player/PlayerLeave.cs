using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLeave : MonoBehaviour
{
    [SerializeField] private PlayableCharacter player;

    public void OnLeave(InputValue value)
    {
        StartCoroutine(DelayRemove());
    }

    private IEnumerator DelayRemove()
    {
        player.transform.position = new Vector3(10000, 10000, 0);

        //If no delay then assignZone does not remove player for some strange reason
        yield return new WaitForSeconds(0.1f);

        GameManager.Instance.RemovePlayer(player.GetPlayerRef());
        PlayerManager.Instance.DestroyPlayer(player.GetPlayerRef());
    }
}
