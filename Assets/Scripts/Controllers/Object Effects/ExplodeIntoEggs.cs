using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeIntoEggs : MonoBehaviour
{
    [SerializeField] private int minNumOfEggs;
    [SerializeField] private int maxNumOfEggs;
    [SerializeField] private bool onDestroy;
    [SerializeField] private bool eggsDespawn;

    [Space]
    [SerializeField] private GameObject basicEgg;
    [SerializeField] private GameObject rareEgg;
    [SerializeField] private GameObject specialEgg;

    [Space]
    [SerializeField] private float rareEggChance;
    [SerializeField] private float specialEggChance;

    private const float EGG_PUSH_FORCE = 2f;

    private void OnDestroy()
    {
        if (onDestroy) Explode();
    }

    public void Explode()
    {
        if (!LevelManager.Instance.EggsCanSpawn) return;

        int numOfEggs = Random.Range(minNumOfEggs, maxNumOfEggs);

        for(int i = 0; i < numOfEggs; i++)
        {
            float chance = Random.Range(0, 100);
            if(chance < specialEggChance)
            {
                SpawnEgg(specialEgg);
            }
            else if(chance < rareEggChance)
            {
                SpawnEgg(rareEgg);
            }
            else
            {
                SpawnEgg(basicEgg);
            }
        }
    }

    private void SpawnEgg(GameObject eggPrefab)
    {
        Vector3 direction = Random.insideUnitCircle;
        Egg egg = Instantiate(eggPrefab, transform.position + direction, Quaternion.identity).GetComponent<Egg>();
        egg.GetRigidBody().AddForce(direction * EGG_PUSH_FORCE, ForceMode2D.Impulse);
        egg.ToggleDespawn(eggsDespawn);
    }
}
