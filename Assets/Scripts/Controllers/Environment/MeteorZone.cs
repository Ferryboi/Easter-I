using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorZone : MonoBehaviour
{
    [SerializeField] private float width;
    [SerializeField] private float height;
    private float halfWidth;
    private float halfHeight;

    [Space]
    [SerializeField] private GameObject meteor;

    [Space]
    [SerializeField] private float minInterval;
    [SerializeField] private float maxInterval;

    private void Start()
    {
        halfWidth = width / 2;
        halfHeight = height / 2;

        StartCoroutine(SpawnMeteors());
    }

    private IEnumerator SpawnMeteors()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));

            if (!LevelManager.Instance.EggsCanSpawn) continue;

            float x = Random.Range(-halfWidth, halfWidth);
            float y = Random.Range(-halfHeight, halfHeight);
            Instantiate(meteor, new Vector3(transform.position.x + x, transform.position.y + y), Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        float halfWidth = width / 2;
        float halfHeight = height / 2;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(-halfWidth, -halfHeight), new Vector3(halfWidth, -halfHeight));
        Gizmos.DrawLine(new Vector3(halfWidth, -halfHeight), new Vector3(halfWidth, halfHeight));
        Gizmos.DrawLine(new Vector3(halfWidth, halfHeight), new Vector3(-halfWidth, halfHeight));
        Gizmos.DrawLine(new Vector3(-halfWidth, halfHeight), new Vector3(-halfWidth, -halfHeight));
    }
}
