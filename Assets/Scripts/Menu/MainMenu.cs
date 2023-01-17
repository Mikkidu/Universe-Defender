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
    [SerializeField] private AudioManager _audioManager;

    
    void Start()
    {
        _inputName.text = DataManager.Instance.cache.playerName;
        
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
