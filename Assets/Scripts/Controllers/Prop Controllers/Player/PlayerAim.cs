using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    [HideInInspector] public bool IsActive = true;
    [SerializeField] private Player player;
    [SerializeField] private Camera playerCam;
    [Space]
    [SerializeField] private PlayerDetector playerFinder;

    private Vector3 inputDirection = Vector3.zero;
    private void Start()
    {
        if (playerCam == null && Camera.main != null) playerCam = Camera.main;
    }

    public void OnLook(InputValue value)
    {
        if (!IsActive || Time.timeScale == 0) return;

        Vector3 inputVal = value.Get<Vector2>();
        inputDirection = new Vector3(inputVal.x, inputVal.y);
    }

    public void OnLookMouse(InputValue value)
    {
        if (!IsActive || Time.timeScale == 0 || playerCam == null) return;

        Vector3 inputVal = value.Get<Vector2>();
        inputVal.z = 10;

        Vector3 worldPos = playerCam.ScreenToWorldPoint(inputVal);
        worldPos.z = transform.position.z;

        inputDirection = worldPos - transform.position;
        inputDirection = inputDirection.normalized;
    }

    public void OnLookController(InputValue value)
    {
        if (!IsActive || Time.timeScale == 0) return;

        inputDirection = value.Get<Vector2>();
    }

    private void Update()
    {
        if (inputDirection.sqrMagnitude != 0) 
        {
            transform.up = inputDirection;
            return;
        }

        if(playerFinder != null)
        {
            Player closestPlayer = playerFinder.GetClosestPlayerNotInTeam(player.GetTeam());
            if (closestPlayer != null)
            {
                Vector3 direction = closestPlayer.transform.position - transform.position;
                transform.up = direction;
                return;
            }
        }

        transform.localRotation = Quaternion.LookRotation(new Vector3(0, 0, -1), new Vector3(0, 1));
    }
}
