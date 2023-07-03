using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGamemodeButton : SetButton<GamemodeRef>
{
    public override void UseButton()
    {
        GameManager.Instance.GetLevelSettings().SetGamemode(buttonValue);
        PlayerPrefs.SetString(PreferenceKeys.GAMEMODE_KEY, buttonValue.ToString());
        PlayerPrefs.Save();

        base.UseButton();
    }
}
