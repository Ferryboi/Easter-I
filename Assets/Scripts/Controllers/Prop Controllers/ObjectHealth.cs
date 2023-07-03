using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private GameObject model;
    public int CurrentHealth { get; private set; }
    public bool IsDead => CurrentHealth <= 0;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = maxHealth;
    }

    public void Damage(int damage)
    {
        CurrentHealth -= damage;
        if(CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Kill();
        }    
    }

    public void Heal(int health)
    {
        int prevHealth = CurrentHealth;

        CurrentHealth += health;
        if (CurrentHealth > maxHealth) CurrentHealth = maxHealth;

        if (prevHealth <= 0 && CurrentHealth > prevHealth) Respawn();
    }

    protected virtual void Kill()
    {
        model.SetActive(false);
    }

    protected virtual void Respawn()
    {
        model.SetActive(true);
    }
}
