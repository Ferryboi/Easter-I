using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsControls : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;

    [Space]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider bgmSlider;

    private void Start()
    {
        SetMasterVolume(GetMasterVolume());
        SetSFXVolume(GetSFXVolume());
        SetBGMVolume(GetBGMVolume());
    }

    private void OnDisable()
    {
        PlayerPrefs.Save();
    }

    public void SetMasterVolume(float volume)
    {
        if(masterSlider) masterSlider.value = volume;
        mixer.SetFloat(PreferenceKeys.VOL_KEY_MASTER, ConvertVolume(volume));
        PlayerPrefs.SetFloat(PreferenceKeys.VOL_KEY_MASTER, volume);
    }

    public float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(PreferenceKeys.VOL_KEY_MASTER);
    }

    public void SetBGMVolume(float volume)
    {
        if (bgmSlider) bgmSlider.value = volume;
        mixer.SetFloat(PreferenceKeys.VOL_KEY_BGM, ConvertVolume(volume));
        PlayerPrefs.SetFloat(PreferenceKeys.VOL_KEY_BGM, volume);
    }

    public float GetBGMVolume()
    {
        return PlayerPrefs.GetFloat(PreferenceKeys.VOL_KEY_BGM);
    }

    public void SetSFXVolume(float volume)
    {
        if (sfxSlider) sfxSlider.value = volume;
        mixer.SetFloat(PreferenceKeys.VOL_KEY_SFX, ConvertVolume(volume));
        PlayerPrefs.SetFloat(PreferenceKeys.VOL_KEY_SFX, volume);
    }

    public float GetSFXVolume()
    {
        return PlayerPrefs.GetFloat(PreferenceKeys.VOL_KEY_SFX);
    }

    private float ConvertVolume(float sliderVol)
    {
        return Mathf.Log10(sliderVol) * 20;
    }
}
