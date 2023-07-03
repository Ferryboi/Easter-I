using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravestoneRespawner : MonoBehaviour
{
    [SerializeField] private ObjectHealth gravestoneHealth;
    [SerializeField] private float healTime;

    private void OnBecameVisible()
    {
        StopAllCoroutines();
    }

    private void OnBecameInvisible()
    {
        if(gravestoneHealth.IsDead && isActiveAndEnabled)
        {
            StartCoroutine(CountdownToRespawn());
        }
    }

    private IEnumerator CountdownToRespawn()
    {
        yield return new WaitForSeconds(healTime);

        gravestoneHealth.Heal(1000);
    }
}
