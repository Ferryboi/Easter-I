using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMapButton : SetButton<MapRef>
{
    public override void UseButton()
    {
        GameManager.Instance.GetLevelSettings().SetMapType(buttonValue);
        PlayerPrefs.SetString(PreferenceKeys.MAP_KEY, buttonValue.ToString());
        PlayerPrefs.Save();

        base.UseButton();
    }
}
