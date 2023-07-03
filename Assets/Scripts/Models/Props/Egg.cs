using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    private static float despawnTime = 20f;
    private Coroutine despawnCoroutine;

    public EggType GetEggType() { return eggType; }
    [SerializeField] private EggType eggType;

    public Rigidbody2D GetRigidBody() { return rigidBody; }
    [SerializeField] private Rigidbody2D rigidBody;

    public EggSpawner GetEggSpawner() { return spawner; }
    public void SetEggSpawner(EggSpawner spawner) { this.spawner = spawner; ToggleDespawn(false); }
    private EggSpawner spawner;

    public int GetEggValue()
    {
        return (int)eggType;
    }

    public void ToggleDespawn(bool shouldDespawn)
    {
        if(shouldDespawn && despawnCoroutine == null)
        {
            despawnCoroutine = StartCoroutine(DespawnTimer());
        }
        else
        {
            if(despawnCoroutine != null) StopCoroutine(despawnCoroutine);
            despawnCoroutine = null;
        }
    }

    private IEnumerator DespawnTimer()
    {
        yield return new WaitForSeconds(despawnTime);

        Destroy(gameObject);
    }
}
