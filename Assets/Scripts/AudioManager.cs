using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _musicBar;
    [SerializeField] private Slider _effectBar;

    private DataManager _dManager;

    private void Start()
    {
        _dManager = DataManager.Instance;
        _musicBar.value = Mathf.Pow(10, _dManager.cache.musicVolume / 40);
        _effectBar.value = Mathf.Pow(10, _dManager.cache.effectVolume / 40);
        SetVolume();
    }

    public void ChangeMusicVolume()
    {
        _dManager.cache.musicVolume = Mathf.Log10(_musicBar.value) * 40;
        Debug.Log($"Mus{_musicBar.value}");
        SetMusicVolume();
    }

    public void ChangeEffectsVolume()
    {
        _dManager.cache.effectVolume = Mathf.Log10(_effectBar.value) * 40;

        SetEffectsVolume();
    }
    public void SetVolume()
    {
        SetMusicVolume();
        SetEffectsVolume();
    }
    public void SetMusicVolume()
    {
        Debug.Log($"Mus{_dManager.cache.musicVolume}");
        _audioMixer.SetFloat("Music", _dManager.cache.musicVolume);

    }
    public void SetEffectsVolume()
    {
        _audioMixer.SetFloat("Effects", _dManager.cache.effectVolume);
    }

}
