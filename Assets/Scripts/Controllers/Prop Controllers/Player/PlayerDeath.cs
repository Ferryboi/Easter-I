using System.Collections;
using UnityEngine;

[System.Serializable]
public class PlayerDeath
{
    public const float DEATH_DURATION = 3;
    public const float RESPAWN_ANIM_DURATION = 4;
    public const float RESPAWN_INVINCIBLE_DURATION = 5;
    public static readonly int RESPAWN_COST = 10;

    private const int MIN_EGG_DROP_ON_KILL = 5;
    private const float POCKET_DROP_FLOOR_PERCENTAGE = 0.1f;
    private const float POCKET_DROP_TOTAL_PERCENTAGE = 0.5f;

    private const float EGG_PUSH_FORCE = 2f;

    [SerializeField] private GameObject models;
    [SerializeField] private GameObject rabbit;
    [SerializeField] private GameObject components;

    [Space]
    public static bool DropBasketOnDeath = false;
    [SerializeField] private GameObject basketPrefab;
    [SerializeField] private GameObject eggPrefab;

    [Space]
    [SerializeField] private AudioSource deathSFX;

    public void KillPlayer(Player player)
    {
        //Show player as dead
        player.IsActive = false;
        player.GetMovement().IsActive = false;
        player.GetPlayerActions().SetActive(false);
        models.SetActive(false);
        components.SetActive(false);

        int droppedEggs;
        if (DropBasketOnDeath)
        {
            int removalAmount = (int)(player.GetPocket().GetEggCount() * POCKET_DROP_TOTAL_PERCENTAGE); 
            
            if (removalAmount < MIN_EGG_DROP_ON_KILL) removalAmount = MIN_EGG_DROP_ON_KILL;
            int totalEggs = player.GetPocket().RemoveFromPocket(removalAmount);
            
            droppedEggs = (int)(totalEggs * POCKET_DROP_FLOOR_PERCENTAGE);

            //Drop a basket at player position
            Basket basket = Object.Instantiate(basketPrefab, player.transform.position, Quaternion.identity).GetComponent<Basket>();
            basket.SetTeam(player.GetTeam());
            basket.AddToPocket(totalEggs - droppedEggs);
        }
        else
        {
            droppedEggs = player.GetPocket().EmptyPocket();
        }

        for (int i = 0; i < droppedEggs; i++)
        {
            Vector3 direction = Random.insideUnitCircle;
            Egg egg = Object.Instantiate(eggPrefab, player.transform.position + direction, Quaternion.identity).GetComponent<Egg>();
            egg.GetRigidBody().AddForce(direction * EGG_PUSH_FORCE, ForceMode2D.Impulse);
            egg.ToggleDespawn(true);
        }

        deathSFX.gameObject.SetActive(true);
        deathSFX.Play();
    }

    public IEnumerator Respawner(Player player)
    {
        yield return new WaitForSeconds(DEATH_DURATION);

        //Wait for respawning to be enabled
        while (!PlayerManager.Instance.CanRespawn) yield return 0;

        player.transform.position = PlayerManager.Instance.GetPlayerSpawnPos(player.GetTeam());

        //Show the player in respawning state
        models.SetActive(true);
        rabbit.SetActive(false);
        player.IsActive = true;
        player.GetHealth().HealPlayer(player.GetHealth().GetMaxHealth());
        player.GetHealth().SetInvincible(true);

        yield return new WaitForSeconds(RESPAWN_ANIM_DURATION);

        //Return movement to the player
        player.GetMovement().IsActive = true;
        player.GetPlayerActions().SetActive(true);
        components.SetActive(true);
        rabbit.SetActive(true);

        player.GetPocket().AddToPocket(RESPAWN_COST);

        //Provide limited invincibility duration
        yield return new WaitForSeconds(RESPAWN_INVINCIBLE_DURATION);
        player.GetHealth().SetInvincible(false);
    }
}
