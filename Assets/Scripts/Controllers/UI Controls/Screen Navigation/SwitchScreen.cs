using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwitchScreen : MonoBehaviour
{
    [SerializeField] private EventSystem eventSystem;
    [Space]
    [SerializeField] private ScreenUI[] screens;
    private ScreenUI currentScreen;

    private void Start()
    {
        for(int i = 0; i < screens.Length; i++)
        {
            screens[i].DeactivateScreen();
        }

        if(screens.Length > 0)
        {
            SetSelected(screens[0]);
        }
    }

    public void NavigateToScreen(string screenName)
    {
        ScreenRef screenRef = (ScreenRef)Enum.Parse(typeof(ScreenRef), screenName);

        NavigateToScreen(screenRef);
    }

    public void NavigateToScreen(ScreenRef screenRef)
    {
        for(int i = 0; i < screens.Length; i++)
        {
            if(screens[i].ScreenRef == screenRef)
            {
                SetSelected(screens[i]);
            }
        }
    }

    private void SetSelected(ScreenUI setScreen)
    {
        if (currentScreen != null) currentScreen.DeactivateScreen();

        GameObject button = setScreen.ActivateScreen();
        currentScreen = setScreen;

        if(button != null) eventSystem.SetSelectedGameObject(button);
    }
}
