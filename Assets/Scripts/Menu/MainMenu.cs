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
    [SerializeField] private TextMeshProUGUI _hiScoreText;

    private DataManager _dManager;

    

    void Start()
    {
        _dManager = DataManager.Instance;
        _inputName.text = _dManager.cache.playerName;
        _hiScoreText.SetText($"Score:\n{_dManager.cache.championName} - {_dManager.cache.hiScore}");
    }




    public void StartGame()
    {
        _dManager.SaveCash();
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        _dManager.SaveCash();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#elif PLATFORM_WEBGL
        SceneManager.LoadScene(0);
#else
        Application.Quit();
#endif
    }
}
