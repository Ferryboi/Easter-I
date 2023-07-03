using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public PlayerRef PlayerRef = PlayerRef.None;

    public Rigidbody2D GetRigidBody() { return rigidBody; }
    [SerializeField] private Rigidbody2D rigidBody;

    public Movement GetMovement() { return movement; }
    [SerializeField] private Movement movement;

    public BulletImpact GetBulletImpact() { return impact; }
    [SerializeField] private BulletImpact impact;
}
