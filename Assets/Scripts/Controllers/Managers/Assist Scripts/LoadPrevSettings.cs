using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class LoadPrevSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    private const float defaultVolSetting = 0.5f;

    private void Start()
    {
        SetGamemode(PlayerPrefs.GetString(PreferenceKeys.GAMEMODE_KEY));
        SetMap(PlayerPrefs.GetString(PreferenceKeys.MAP_KEY));

        SetVolume(PreferenceKeys.VOL_KEY_MASTER);
        SetVolume(PreferenceKeys.VOL_KEY_SFX);
        SetVolume(PreferenceKeys.VOL_KEY_BGM);

        SetRoundDuration(PreferenceKeys.ROUND_DURATION);
    }

    private void SetGamemode(string gamemodeRef)
    {
        if (gamemodeRef == null) return;

        GamemodeRef gamemode = (GamemodeRef)Enum.Parse(typeof(GamemodeRef), gamemodeRef);
        GameManager.Instance.GetLevelSettings().SetGamemode(gamemode);
    }

    private void SetMap(string mapRef)
    {
        if (mapRef == null) return;

        MapRef map = (MapRef)Enum.Parse(typeof(MapRef), mapRef);
        GameManager.Instance.GetLevelSettings().SetMapType(map);
    }

    private void SetVolume(string key)
    {
        float volume = defaultVolSetting;
        if (PlayerPrefs.HasKey(key)) volume = PlayerPrefs.GetFloat(key); 

        mixer.SetFloat(key, ConvertVolume(volume));
    }

    private float ConvertVolume(float sliderVol)
    {
        return Mathf.Log10(sliderVol) * 20;
    }

    private void SetRoundDuration(string key)
    {
        float durationTime = PlayerPrefs.GetFloat(key);

        if (durationTime != 0)
        {
            GameManager.Instance.GetLevelSettings().SetRoundDuration(durationTime);
        }
        else
        {
            GameManager.Instance.GetLevelSettings().SetRoundDuration(LevelSettings.DEFAULT_ROUND_DURATION);
        }
    }
}
