using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CreateNewPlayer : MonoBehaviour
{
    [SerializeField] private PlayerInput input;

    private void Awake()
    {
        GameManager.Instance.AddPlayer(input);
        Destroy(gameObject);
    }
}
