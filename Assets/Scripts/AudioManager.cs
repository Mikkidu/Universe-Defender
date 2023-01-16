using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;

    private void Start()
    {
        SetVolume();
    }

    public void SetVolume()
    {
        SetMusicVolume();
        SetEffectsVolume();
    }
    public void SetMusicVolume()
    {
        Debug.Log($"Музыка{DataManager.Instance.cache.musicVolume}");
        _audioMixer.SetFloat("Music", DataManager.Instance.cache.musicVolume);

    }
    public void SetEffectsVolume()
    {
        Debug.Log($"Effects{DataManager.Instance.cache.effectVolume}");
        _audioMixer.SetFloat("Effects", DataManager.Instance.cache.effectVolume);
    }

}
