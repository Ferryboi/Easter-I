using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [HideInInspector] public bool IsActive = true;
    public bool CanRespawn = true;

    [SerializeField] private Player player;

    public bool IsInvincible { get; private set; }
    private const float INVINCIBLE_TRANSPARANCY = 0.75f;

    public int GetMaxHealth() { return maxHealth; }
    [SerializeField] private int maxHealth = 3;

    public int GetHealth() { return currentHealth; }
    private int currentHealth;

    public delegate void OnChangeDelegate(int change);
    public OnChangeDelegate OnChange;

    public delegate void OnDeathDelegate(Player player);
    public static OnDeathDelegate OnDeath;

    public static bool OverrideDeathController = false;
    [SerializeField] private PlayerDeath deathController;

    public int GetNumOfDeaths() { return numOfDeaths; }
    private int numOfDeaths;

    private void Awake()
    {
        currentHealth = maxHealth;
        numOfDeaths = 0;
    }

    public int HealPlayer(int health = 1)
    {
        if (!IsActive) return currentHealth;

        int healingAmount = currentHealth + health > maxHealth ? maxHealth - currentHealth : health;

        currentHealth += healingAmount;

        OnChange?.Invoke(healingAmount);
        return currentHealth;
    }

    public int DamagePlayer(int damage = 1)
    {
        if (!IsActive || IsInvincible) return currentHealth;

        int damageAmount = currentHealth - damage < 0 ? currentHealth : damage;

        currentHealth -= damageAmount;

        OnChange?.Invoke(-damageAmount);

        if(currentHealth == 0) KillPlayer();

        return currentHealth;
    }

    public bool KillPlayer()
    {
        if (!IsActive || IsInvincible) return false;

        numOfDeaths++;
        if (!OverrideDeathController)
        {
            deathController.KillPlayer(player);
            if(CanRespawn) StartCoroutine(deathController.Respawner(player));
        }

        OnDeath?.Invoke(player);
        return true;
    }

    public void SetInvincible(bool invincible)
    {
        IsInvincible = invincible;
    }
}
