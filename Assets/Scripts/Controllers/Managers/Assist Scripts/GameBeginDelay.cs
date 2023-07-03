using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBeginDelay : MonoBehaviour
{
    [SerializeField] private float duration;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(duration);

        LevelManager.Instance.StartGame();
        PlayerManager.Instance.SpawnPlayers(GameManager.Instance.GetPlayers());
    }
}
