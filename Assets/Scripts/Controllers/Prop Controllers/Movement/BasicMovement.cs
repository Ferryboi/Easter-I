using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : Movement
{
    public override void Move(float scale = 1f)
    {
        if (!IsActive) return;
        IsMoving = true;

        _movement = _direction * _speed * scale * Time.deltaTime;
        transform.position += _movement;
        _lastValidPosition = transform.position;

        OnMovementChange?.Invoke();
    }
}
