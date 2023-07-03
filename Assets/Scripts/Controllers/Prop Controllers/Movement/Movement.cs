using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [SerializeField] protected float _speed;
    [SerializeField] protected bool _faceMovement;

    [HideInInspector] public bool IsActive = true;
    public bool IsMoving { get; protected set; }

    protected Vector3 _direction;
    protected Vector3 _movement;
    protected Vector3 _lastValidPosition;

    public delegate void OnMovementChangeDelegate();
    public OnMovementChangeDelegate OnMovementChange;

    public void SetDirection(Vector3 direction)
    {
        if (direction.sqrMagnitude > 1) direction.Normalize();
        _direction = direction;

        if (_faceMovement) FaceDirection();
    }

    public void SetFaceMovement(bool faceMovement)
    {
        _faceMovement = faceMovement;
    }

    public Vector3 GetDirection()
    {
        return _direction;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public float GetSpeed()
    {
        return _speed;
    }

    protected void StopMovement()
    {
        IsMoving = false;
        _direction = Vector3.zero;
        _movement = Vector3.zero;

        OnMovementChange?.Invoke();
    }

    public abstract void Move(float scale = 1f);

    public Vector3 GetMovement()
    {
        return _movement;
    }

    public Vector3 GetLastValidPosition()
    {
        return _lastValidPosition;
    }

    protected virtual void FaceDirection()
    {
        transform.up = _direction;
    }
}
