using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Transform bulletShootPos;
    
    public bool IsActive => isActive;
    private bool isActive = true;

    [Space]
    public bool CanInteract;
    public bool CanAttack;
    [Space]
    [SerializeField] private GameObject smallBulletPrefab;
    [SerializeField] private int smallBulletCost = 1;
    [SerializeField] private GameObject largeBulletPrefab;
    [SerializeField] private int largeBulletCost = 5;
    public bool InfiniteAmmo;

    private bool isAttacking;
    private bool isCharged;

    [Space]
    [SerializeField] private Animator animator;
    [SerializeField] private string shootingAnim;

    [Space]
    [SerializeField] private AudioSource regularShotSFX;
    [SerializeField] private AudioSource largeShotSFX;
    [SerializeField] private AudioSource whiffedShotSFX;

    private List<IInteractable> interactables = new List<IInteractable>();

    #region Input Actions
    public void OnAction(InputValue value)
    {
        if (!isActive || Time.timeScale == 0) return;

        bool pressed = value.Get<float>() == 1;
        bool released = value.Get<float>() == 0;

        if (pressed)
        {
            ActionPressed();
        }
        else if (released)
        {
            ActionReleased();
        }
    }

    public void OnActionHold(InputValue value)
    {
        if (!isActive || Time.timeScale == 0) return;

        bool pressed = value.Get<float>() == 1;
        bool released = value.Get<float>() == 0;

        if (pressed)
        {
            ActionHeldStart();
        }
        else if (released)
        {
            ActionHeldEnd();
        }
    }
    #endregion

    #region On Press/Release
    private void ActionPressed()
    {
        if (InteractAvailable())
        {
            for(int i = 0; i < interactables.Count; i++) interactables[i].OnInteract(player);
        }
        else if (CanAttack)
        {
            isAttacking = true;
            animator.SetBool("charging", isAttacking);
        }
    }

    private void ActionReleased()
    {
        if (CanAttack && isAttacking)
        {
            if (isCharged && (player.GetPocket().GetEggCount() >= largeBulletCost || InfiniteAmmo))
            {
                largeShotSFX.Play();
                SpawnBullet(largeBulletPrefab, largeBulletCost);
            }
            else if (player.GetPocket().GetEggCount() >= smallBulletCost || InfiniteAmmo)
            {
                if(isCharged) whiffedShotSFX.Play();
                else regularShotSFX.Play();

                SpawnBullet(smallBulletPrefab, smallBulletCost);
            }
            else
            {
                whiffedShotSFX.Play();

                //Reset animator
                animator.Rebind();
                animator.Update(0);
            }
        }

        isAttacking = false;
        isCharged = false;

        animator.SetBool("charging", isAttacking);
        animator.SetBool("charged", isCharged);
    }
    #endregion

    #region On Hold
    private void ActionHeldStart()
    {
        if (InteractAvailable())
        {
            for (int i = 0; i < interactables.Count; i++) interactables[i].OnInteractHoldStart(player);
        }
        else if (CanAttack && isAttacking)
        {
            isCharged = true;
            animator.SetBool("charged", isCharged);
        }
    }

    private void ActionHeldEnd()
    {
        if (InteractAvailable())
        {
            for (int i = 0; i < interactables.Count; i++) interactables[i].OnInteractHoldEnd(player);
        }
        else if (CanAttack)
        {

        }
    }
    #endregion


    public void SetInteractable(IInteractable interactable)
    {
        if (!interactables.Contains(interactable))
        {
            interactables.Add(interactable);
        }
    }

    public void RemoveInteractable(IInteractable interactable)
    {
        if (interactables.Contains(interactable))
        {
            interactables.Remove(interactable);
        }
    }

    private bool InteractAvailable() 
    {
        return CanInteract && interactables.Count > 0; 
    }

    public void SetActive(bool active)
    {
        isActive = active;

        if(!isActive)
        {
            
        }
    }

    private void SpawnBullet(GameObject bulletPrefab, int cost)
    {
        player.GetPocket().RemoveFromPocket(cost);

        Bullet bullet = Instantiate(bulletPrefab, bulletShootPos.position, player.transform.rotation).GetComponent<Bullet>();
        bullet.PlayerRef = player.GetPlayerRef();
        bullet.GetMovement().SetDirection(bulletShootPos.up);

        animator.Play(shootingAnim);
    }
}
