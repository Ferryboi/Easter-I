using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportInRadius : MonoBehaviour
{
    [SerializeField] private float radius = 1f;
    [Space]
    [SerializeField] private bool moveOnInterval = false;
    [SerializeField] private float minInterval = 2f;
    [SerializeField] private float maxInterval = 10f;

    private void Awake()
    {
        if (moveOnInterval) StartCoroutine(MoveLoop());
    }

    private IEnumerator MoveLoop()
    {
        while(moveOnInterval)
        {
            float interval = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(interval);

            Vector3 pos = transform.position + (Vector3)(Random.insideUnitCircle.normalized * radius);
            TeleportToPos(pos);
        }
    }

    public void TeleportToPos(Vector3 pos)
    {
        transform.position = pos;
    }
}
