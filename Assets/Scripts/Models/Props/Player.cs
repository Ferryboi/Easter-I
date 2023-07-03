using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : PlayableCharacter
{
    public Rigidbody2D GetRigidbody() { return rigidBody; }
    [SerializeField] private Rigidbody2D rigidBody;

    public PlayerHealth GetHealth() { return health; }
    [Header("Gameplay Assets")] [SerializeField] private PlayerHealth health;

    public Movement GetMovement() { return movement; }
    [SerializeField] private Movement movement;

    public Pocket GetPocket() { return pocket; }
    [SerializeField] private Pocket pocket;

    public PlayerActions GetPlayerActions() { return actions; }
    [SerializeField] private PlayerActions actions;
}
