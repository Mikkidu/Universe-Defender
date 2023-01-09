using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSounds : MonoBehaviour
{
    [SerializeField] private AudioClip _explosion;
    [SerializeField] private AudioClip _buttonSound;
    private AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    
    public void PlayButtonSound()
    {
        PlaySound(_buttonSound);
    }

    public void PlayExplosionSound()
    {
        PlaySound(_explosion);
    }

    private void PlaySound(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}
