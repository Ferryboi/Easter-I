using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerReloadScene : MonoBehaviour
{
    public void OnPause(InputValue value)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
