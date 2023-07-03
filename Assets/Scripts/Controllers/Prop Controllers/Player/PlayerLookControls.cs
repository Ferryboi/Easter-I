using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLookControls : MonoBehaviour
{
    [HideInInspector] public bool IsActive = true;
    [SerializeField] private Player player;
    [SerializeField] private Camera playerCam;
    [SerializeField] private Transform model;
    [SerializeField] private Transform defaultModelToFollow;

    private Vector3 lookDirection = Vector3.zero;

    private void Start()
    {
        if (playerCam == null && Camera.main != null) playerCam = Camera.main;
    }

    private void Update()
    {
        if (lookDirection.sqrMagnitude != 0)
        {
            model.up = lookDirection;
        }
        else
        {
            model.up = defaultModelToFollow.up;
        }
    }

    public void OnLook(InputValue value)
    {
        if (!IsActive || Time.timeScale == 0) return;

        Vector3 inputVal = value.Get<Vector2>();
        lookDirection = new Vector3(inputVal.x, inputVal.y);
    }

    public void OnLookMouse(InputValue value)
    {
        if (!IsActive || Time.timeScale == 0 || playerCam == null) return;

        return;
        //THIS CODE IS NOT WORKING WITH SPLIT SCREEN

        Vector3 inputVal = value.Get<Vector2>();

        Vector3 worldPos = playerCam.ScreenToWorldPoint(inputVal);
        worldPos.z = model.position.z;

        lookDirection = worldPos - model.position;
        lookDirection = lookDirection.normalized;
    }

    public void OnLookController(InputValue value)
    {
        if (!IsActive || Time.timeScale == 0) return;

        Vector3 inputVal = value.Get<Vector2>();
        lookDirection = new Vector3(inputVal.x, inputVal.y);
    }
}
