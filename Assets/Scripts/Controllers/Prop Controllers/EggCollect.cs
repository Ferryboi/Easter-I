using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggCollect : MonoBehaviour
{
    [SerializeField] private Egg egg;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponentInParent<Player>();

        if(player != null)
        {            
            player.GetPocket().AddToPocket(egg.GetEggValue());

            if (egg.GetEggSpawner())
            {
                egg.GetEggSpawner().RemoveEgg();
            }
            else
            {
                Destroy(egg.gameObject);
            }
        }
    }
}
