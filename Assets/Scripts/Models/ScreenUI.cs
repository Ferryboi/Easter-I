using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ScreenUI
{
    public ScreenRef ScreenRef => screenRef;
    [SerializeField] private ScreenRef screenRef;

    [SerializeField] private GameObject screen;
    [SerializeField] private GameObject firstSelected;

    public GameObject ActivateScreen()
    {
        screen.SetActive(true);
        return firstSelected;
    }

    public void DeactivateScreen()
    {
        screen.SetActive(false);
    }
}
