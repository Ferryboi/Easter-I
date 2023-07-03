using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSpawner : MonoBehaviour
{
    public EggType GetEggType() { return eggType; }
    [SerializeField] private EggType eggType;
    [SerializeField] private GameObject eggPrefab;

    [Space]
    [SerializeField] private float minRespawnTime;
    [SerializeField] private float maxRespawnTime;

    public Egg GetEgg() { return egg; }
    private Egg egg;

    [Space]
    [SerializeField] private bool spawnOnStart;
    public bool Respawn;

    private void Start()
    {
        egg = Instantiate(eggPrefab, transform).GetComponent<Egg>();
        egg.SetEggSpawner(this);

        if (!spawnOnStart || !LevelManager.Instance.EggsCanSpawn) RemoveEgg();
    }

    public void SpawnEgg()
    {
        if(LevelManager.Instance.EggsCanSpawn)
        {
            egg.gameObject.SetActive(true);
        }
    }

    public void RemoveEgg()
    {
        if(egg.gameObject.activeInHierarchy)
        {
            egg.gameObject.SetActive(false);
            if(Respawn) StartCoroutine(RespawnTimer(Random.Range(minRespawnTime, maxRespawnTime)));
        }    
    }

    private IEnumerator RespawnTimer(float respawnTime)
    {
        yield return new WaitForSeconds(respawnTime);

        //while (LevelManager.Instance.EggsCanSpawn == false) yield return 0;
        SpawnEgg();
    }

    public void OnDisable()
    {
        RemoveEgg();
    }
}
