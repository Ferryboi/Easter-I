using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponentInParent<Player>();
        if(player != null)
        {
            player.GetHealth().DamagePlayer(damage);
        }

        ObjectHealth objectHealth = collision.GetComponentInParent<ObjectHealth>();
        if(objectHealth != null)
        {
            objectHealth.Damage(damage);
        }

        Destroy(bullet.gameObject);
    }
}
