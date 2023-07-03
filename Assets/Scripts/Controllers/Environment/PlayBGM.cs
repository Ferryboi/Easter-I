using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBGM : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private bool forceRestart;

    private void Start()
    {
        if (forceRestart) GameManager.Instance.StopBGM();
        GameManager.Instance.PlayBGM(clip);
    }

    public void SetMusic(AudioClip clip)
    {
        this.clip = clip;
    }

    public void PlayMusic()
    {
        GameManager.Instance.PlayBGM(clip);
    }
}
