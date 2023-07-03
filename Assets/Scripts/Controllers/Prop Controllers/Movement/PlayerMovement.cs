using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : BasicMovement
{
    private void LateUpdate()
    {
        if(IsMoving)
        {
            Move();
        }
    }

    public void OnMove(InputValue value)
    {
        if (!IsActive) return;

        Vector3 direction = value.Get<Vector2>();

        if ((direction.x == 0 && direction.y == 0) || Time.timeScale == 0)
        {
            StopMovement();
            return;
        }
        
        IsMoving = true;
        SetDirection(direction);
    }

    protected override void FaceDirection()
    {
        transform.up = _direction;
    }
}
