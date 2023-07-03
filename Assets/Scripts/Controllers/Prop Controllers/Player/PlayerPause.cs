using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPause : MonoBehaviour
{
    public void OnPause(InputValue value)
    {
        GameUIManager.Instance.TogglePause();
    }
}
