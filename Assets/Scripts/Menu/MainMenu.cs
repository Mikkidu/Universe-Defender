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

    
    void Start()
    {
        _inputName.text = DataManager.Instance.cache.playerName;
        _musicBar.value = DataManager.Instance.cache.musicVolume;
        _effectBar.value = DataManager.Instance.cache.effectVolume;
    }

    public void ChangeMusicVolume()
    {
        DataManager.Instance.cache.musicVolume = _musicBar.value;
    }

    public void ChangeEffectsVolume()
    {
        DataManager.Instance.cache.effectVolume = _effectBar.value;
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
