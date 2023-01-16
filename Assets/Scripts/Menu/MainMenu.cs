using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_InputField _inputName;
    [SerializeField] private Slider _musicBar;
    [SerializeField] private Slider _effectBar;
    [SerializeField] private AudioManager _audioManager;

    
    void Start()
    {
        _inputName.text = DataManager.Instance.cache.playerName;
        _musicBar.value = Mathf.Pow(10, DataManager.Instance.cache.musicVolume / 40);
        _effectBar.value = Mathf.Pow(10, DataManager.Instance.cache.effectVolume / 40);
        
    }

    public void ChangeMusicVolume()
    {
        DataManager.Instance.cache.musicVolume = Mathf.Log10(_musicBar.value) * 40;
        Debug.Log($"MusicBar{_musicBar.value}");
        _audioManager.SetMusicVolume();
    }

    public void ChangeEffectsVolume()
    {
        DataManager.Instance.cache.effectVolume = Mathf.Log10(_effectBar.value) * 40;
        Debug.Log($"MusicBar{_effectBar.value}");
        _audioManager.SetEffectsVolume();
    }


    public void StartGame()
    {
        DataManager.Instance.SaveCash();
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        DataManager.Instance.SaveCash();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#elif PLATFORM_WEBGL
        SceneManager.LoadScene(0);
#else
        Application.Quit();
#endif
    }
}
