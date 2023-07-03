using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAtCenterPos : MonoBehaviour
{
    [SerializeField] private GameObject spawnPrefab;

    private void SpawnPrefab()
    {
        Vector3 pos = LevelManager.Instance.GetCenterPos();

        Instantiate(spawnPrefab, pos, Quaternion.identity);
    }
}
